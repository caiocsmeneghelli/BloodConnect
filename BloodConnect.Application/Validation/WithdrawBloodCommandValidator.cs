using BloodConnect.Application.Commands.WithdrawBlood;
using BloodConnect.Domain.UnitOfWork;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
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
                .NotEmpty()
                .WithMessage("Id não pode ser vazio.");
            RuleFor(reg => reg.IdBloodStock)
                .MustAsync(BloodstockExist)
                .WithMessage("Estoque de sangue não encontrado.");
            
            RuleFor(reg => reg.QuantityMl)
                .NotEmpty()
                .WithMessage("Quantidade para requisição deve ser maior que 0");
            
            RuleFor(reg => reg)
                .MustAsync(QuantityMustBeLessThanStock)
                .WithMessage("Quantidade de requisição deve ser menor que quantidade em estoque.");
        }

        private async Task<bool> BloodstockExist(int idBloodStock, CancellationToken cancellationToken){
            var bloodStock = await _unitOfWork.BloodStocks.GetByIdAsync(idBloodStock);
            return bloodStock is not null;
        }

        private async Task<bool> QuantityMustBeLessThanStock(WithdrawBloodCommand command, CancellationToken cancellationToken)
        {
            var bloodStock = await _unitOfWork.BloodStocks.GetByIdAsync(command.IdBloodStock);
            if(bloodStock is null){
                return false;
            }

            return command.QuantityMl <= bloodStock.QuantityMl;
        }
    }
}
