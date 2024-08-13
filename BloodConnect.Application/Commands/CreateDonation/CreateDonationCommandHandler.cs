using BloodConnect.Application.Validation;
using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Repositories;
using BloodConnect.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Commands.CreateDonation
{
    public class CreateDonationCommandHandler : IRequestHandler<CreateDonationCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDonationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDonationCommandValidator();
            var validatorResult = validator.Validate(request);
            if (!validatorResult.IsValid)
            {
                List<string> errors = validatorResult.Errors.Select(reg => reg.ErrorMessage).ToList();
                throw new Exception(errors.ToString());
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

            return donation.Id;
        }
    }
}
