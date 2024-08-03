using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Queries.GetAllDonation
{
    public class GetAllDonationQueryHandler : IRequestHandler<GetAllDonationQuery, List<Donation>>
    {
        private readonly IDonationRepository _donationRepository;

        public GetAllDonationQueryHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        public async Task<List<Donation>> Handle(GetAllDonationQuery request, CancellationToken cancellationToken)
        {
            return await _donationRepository.GetAll();
        }
    }
}
