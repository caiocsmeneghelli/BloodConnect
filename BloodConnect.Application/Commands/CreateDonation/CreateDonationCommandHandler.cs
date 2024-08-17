using BloodConnect.Application.Validation;
using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Repositories;
using BloodConnect.Domain.UnitOfWork;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Commands.CreateDonation
{
    public class CreateDonationCommandHandler : IRequestHandler<CreateDonationCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDonationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDonationCommandValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure(request, validationResult.Errors
                    .Select(reg => reg.ErrorMessage).ToList());
            }


            Donor? donor = await _unitOfWork.Donors.GetAsync(request.IdDonor);
            BloodStock? bloodStock = await _unitOfWork.BloodStocks
                .GetByTypeAndRhFactorAsync(donor.BloodType, donor.RhFactor);

            if (bloodStock == null)
            {
                bloodStock = new BloodStock(donor.BloodType, donor.RhFactor);
            }

            bloodStock.AddMl(request.QuantityMl);
            
            Donation donation = new Donation(donor.Id, request.QuantityMl);


            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Donations.Create(donation);
            if(bloodStock.Id == 0)
            {
                await _unitOfWork.BloodStocks.CreateBloodStock(bloodStock);
            }
            else
            {
                await _unitOfWork.BloodStocks.UpdateBloodStock(bloodStock);
            }

            await _unitOfWork.CompletAsync();

            return Result.Success(donation.Id);
        }
    }
}
