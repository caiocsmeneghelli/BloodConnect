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
    public class GetAllDonorsQuery : IRequest<List<Donor>>
    {
    }
}
