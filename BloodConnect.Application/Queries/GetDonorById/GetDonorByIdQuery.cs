using BloodConnect.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Queries.GetDonorById
{
    public class GetDonorByIdQuery : IRequest<Donor>
    {
        public GetDonorByIdQuery(int idDonor)
        {
            IdDonor = idDonor;
        }

        public int IdDonor { get; set; }
    }
}
