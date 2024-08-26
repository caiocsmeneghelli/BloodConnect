using BloodConnect.Application.Validation;
using BloodConnect.Domain.Entities;
using BloodConnect.Domain.UnitOfWork;
using FluentValidation;
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
            var validator = new WithdrawBloodCommandValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure(request, validationResult.Errors
                    .Select(reg => reg.ErrorMessage).ToList());
            }

            var bloodStock = await _unitOfWork.BloodStocks
                .GetByIdAsync(request.IdBloodStock);

            bloodStock.Withdraw(request.QuantityMl);
            await _unitOfWork.CompletAsync();

            return Result.Success(bloodStock);
        }
    }
}
