using BloodConnect.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Queries.GetDonationById
{
    public class GetDonationByIdQuery : IRequest<Donation?>
    {
        public int IdDonation { get; private set; }

        public GetDonationByIdQuery(int idDonation)
        {
            IdDonation = idDonation;
        }
    }
}
