using BloodConnect.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Commands.WithdrawBlood
{
    public class WithdrawBloodCommandHandler : IRequestHandler<WithdrawBloodCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public WithdrawBloodCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(WithdrawBloodCommand request, CancellationToken cancellationToken)
        {
            var bloodStock = await _unitOfWork.BloodStocks.GetByIdAsync(request.IdBloodStock);

            if(bloodStock is null)
                return Result.Failure(request, )
            throw new NotImplementedException();
        }
    }
}
