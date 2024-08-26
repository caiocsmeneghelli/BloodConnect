using BloodConnect.Application.Commands.WithdrawBlood;
using BloodConnect.Domain.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Validation
{
    public class WithdrawBloodCommandValidator : AbstractValidator<WithdrawBloodCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public WithdrawBloodCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(reg => reg.IdBloodStock)
                .Must()
        }

        private async Task<bool> Exist
    }
}
