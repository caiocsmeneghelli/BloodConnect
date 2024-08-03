using BloodConnect.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Queries.GetAllDonation
{
    public class GetAllDonationQuery : IRequest<List<Donation>>
    {
    }
}
