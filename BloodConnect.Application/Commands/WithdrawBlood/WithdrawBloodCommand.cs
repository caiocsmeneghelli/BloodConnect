using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Commands.WithdrawBlood
{
    public class WithdrawBloodCommand : IRequest<Result>
    {
        public int IdBloodStock { get; private set; }
        public int QuantityMl { get; private set; }
    }
}
