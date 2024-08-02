using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Repositories;
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
        private readonly IDonationRepository _donationRepository;
        private readonly IBloodStockRepository _bloodStockRepository;
        private readonly IDonorRepository _donorRepository;

        public CreateDonationCommandHandler(IDonationRepository donationRepository, 
                                            IBloodStockRepository bloodStockRepository, 
                                            IDonorRepository donorRepository)
        {
            _donationRepository = donationRepository;
            _bloodStockRepository = bloodStockRepository;
            _donorRepository = donorRepository;
        }

        public async Task<int> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            // add validation
            Donor? donor = await _donorRepository.GetAsync(request.IdDonor);
            BloodStock? bloodStock = await _bloodStockRepository
                .GetByTypeAndRhFactorAsync(donor.BloodType, donor.RhFactor);

            if (bloodStock == null)
            {
                bloodStock = new BloodStock(donor.BloodType, donor.RhFactor);
            }

            bloodStock.AddMl(request.QuantityMl);
            
            Donation donation = new Donation(donor.Id, request.QuantityMl);

            await _donationRepository.Create(donation);
            if(bloodStock.Id == 0)
            {
                await _bloodStockRepository.CreateBloodStock(bloodStock);
            }
            else
            {
                await _bloodStockRepository.UpdateBloodStock(bloodStock);
            }

            return donation.Id;
        }
    }
}
