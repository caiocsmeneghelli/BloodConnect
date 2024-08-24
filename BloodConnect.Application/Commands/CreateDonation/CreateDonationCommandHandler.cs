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
            var validator = new CreateDonationCommandValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure(request, validationResult.Errors
                    .Select(reg => reg.ErrorMessage).ToList());
            }


            Donor? donor = await _unitOfWork.Donors.GetAsync(request.IdDonor);
            BloodStock? bloodStock = await _unitOfWork.BloodStocks
                .GetByTypeAndRhFactorAsync(donor.BloodType, donor.RhFactor);

            Donation donation = new Donation(donor, request.QuantityMl);

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Donations.Create(donation);
            await _unitOfWork.CompletAsync();
            if (bloodStock == null)
            {
                bloodStock = new BloodStock(donor.BloodType, donor.RhFactor);
                await _unitOfWork.BloodStocks.CreateBloodStock(bloodStock);
                await _unitOfWork.CompletAsync();
            }

            bloodStock.AddMl(request.QuantityMl);
            await _unitOfWork.CompletAsync();

            await _unitOfWork.CommitAsync();

            return Result.Success(donation.Id);
        }
    }
}
