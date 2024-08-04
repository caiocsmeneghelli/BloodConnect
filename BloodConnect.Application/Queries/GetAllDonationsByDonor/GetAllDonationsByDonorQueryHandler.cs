using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Queries.GetAllDonationsByDonor
{
    public class GetAllDonationsByDonorQueryHandler : IRequestHandler<GetAllDonationsByDonorQuery, List<Donation>>
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IDonorRepository _donorRepository;

        public GetAllDonationsByDonorQueryHandler(IDonationRepository donationRepository, IDonorRepository donorRepository)
        {
            _donationRepository = donationRepository;
            _donorRepository = donorRepository;
        }

        public async Task<List<Donation>> Handle(GetAllDonationsByDonorQuery request, CancellationToken cancellationToken)
        {
            var donor = await _donorRepository.GetAsync(request.IdDonor);
            if(donor == null)
            {
                throw new Exception("Doador não encontrado.");
            }

            return await _donationRepository.GetAllByDonor(request.IdDonor);
        }
    }
}
