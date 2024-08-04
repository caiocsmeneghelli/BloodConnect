using BloodConnect.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Queries.GetAllDonationsByDonor
{
    public class GetAllDonationsByDonorQuery : IRequest<List<Donation>>
    {
        public int IdDonor { get; private set; }

        public GetAllDonationsByDonorQuery(int idDonor)
        {
            IdDonor = idDonor;
        }
    }
}
