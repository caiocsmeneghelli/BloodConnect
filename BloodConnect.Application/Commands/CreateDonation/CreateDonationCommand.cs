using BloodConnect.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Commands.CreateDonation
{
    public class CreateDonationCommand : IRequest<Result>
    {
        public int IdDonor { get; set; }
        public int QuantityMl { get; set; }
    }
}
