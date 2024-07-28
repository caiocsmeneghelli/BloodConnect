using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Queries.GetAllDonors
{
    public class GetAllDonorsQueryHandler : IRequestHandler<GetAllDonorsQuery, List<Donor>>
    {
        private readonly IDonorRepository _donorRepository;
        public GetAllDonorsQueryHandler(IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
        }

        public async Task<List<Donor>> Handle(GetAllDonorsQuery request, 
            CancellationToken cancellationToken)
        {
            return await _donorRepository.GetAllAsync();
        }
    }
}
