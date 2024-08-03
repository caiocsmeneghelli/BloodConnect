using BloodConnect.Application.Commands.CreateDonation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Validation
{
    public class CreateDonationCommandValidator : AbstractValidator<CreateDonationCommand>
    {
        public CreateDonationCommandValidator()
        {
            RuleFor(reg => reg.IdDonor)
                .NotEmpty()
                .WithMessage("O campo Id do doador não pode ser vazio.");
            RuleFor(reg => reg.QuantityMl).GreaterThan(0)
                .WithMessage("A quantidade de sangue doado deve ser maior que zero.");
        }
    }
}
