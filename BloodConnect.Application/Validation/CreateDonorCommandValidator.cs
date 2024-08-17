using BloodConnect.Application.Commands.CreateDonor;
using BloodConnect.Domain.Enums;
using BloodConnect.Domain.UnitOfWork;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BloodConnect.Application.Validation
{
    public class CreateDonorCommandValidator : AbstractValidator<CreateDonorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDonorCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(reg => reg.FullName)
                .NotEmpty()
                .WithMessage("Campo Nome Completo não pode ser vazio.");

            RuleFor(reg => reg.Email)
               .NotEmpty()
               .WithMessage("Campo E-mail não pode ser vazio.");
            When(reg => !string.IsNullOrWhiteSpace(reg.Email), () =>
            {
                RuleFor(reg => reg.Email)
                    .EmailAddress()
                    .WithMessage("E-mail informado não válido.")
                    .MustAsync(ValidateEmailDuplicate)
                    .WithMessage("E-mail já cadastrado.");
            });

            RuleFor(reg => reg.BirthDate)
                .NotEmpty()
                .WithMessage("Campo Data de Nascimento não pode ser vazio.");
            When(reg => reg.BirthDate != null, () =>
            {
                RuleFor(reg => reg.BirthDate)
                    .Must(ValidateBirthdate)
                    .WithMessage("O doador precisa ter mais de 18 anos.");
            });

            RuleFor(reg => reg.Genre)
                .NotEmpty()
                .WithMessage("Campo Genero não pode ser vazio.");
            When(reg => reg.Genre != null, () =>
            {
                RuleFor(reg => reg.Genre)
                    .Must(ValidateEnumGenre)
                    .WithMessage("Campo Genero inválido.");
            });

            RuleFor(reg => reg.Weight)
                .GreaterThanOrEqualTo(50)
                .WithMessage("O peso deve ser de no mínimo 50 kilos.");

            RuleFor(reg => reg.BloodType)
                .NotEmpty()
                .WithMessage("Campo Tipo Sanguineo não pode ser vazio.");
            When(reg => reg.BloodType != null, () =>
            {
                RuleFor(reg => reg.BloodType)
                    .Must(ValidateEnumBloodType)
                    .WithMessage("Campo Tipo Sanguineo inválido.");
            });

            RuleFor(reg => reg.RhFactor)
                .NotEmpty()
                .WithMessage("Campo FatorRh não pode ser vazio.");
            When(reg => reg.RhFactor != null, () =>
            {
                RuleFor(reg => reg.RhFactor)
                    .Must(ValidateEnumRhFactor)
                    .WithMessage("Campo FatorRh inválido.");
            });

            RuleFor(reg => reg.Cep)
                .NotEmpty()
                .WithMessage("Campo Cep não pode ser vazio.");
            When(reg => reg.Cep != null, () =>
            {
                RuleFor(reg => reg.Cep)
                    .Must(ValidateCep)
                    .WithMessage("Campo Cep inválido.");
            });


        }

        private bool ValidateBirthdate(DateTime? birthDate)
        {
            DateTime birthDateLegal = DateTime.Now.AddYears(-18);
            return birthDate < birthDateLegal;
        }

        private bool ValidateEnumGenre(string emumGenre)
        {
            return Enum.TryParse<Genre>(emumGenre, out var genreEnum);
        }

        private bool ValidateEnumBloodType(string emumBloodType)
        {
            return Enum.TryParse<BloodType>(emumBloodType, out var bloodTypeEnum);
        }

        private bool ValidateEnumRhFactor(string emumRhFactor)
        {
            return Enum.TryParse<RhFactor>(emumRhFactor, out var rhFactorEnum);
        }

        private bool ValidateCep(string cep)
        { 
            string cepNumeros = Regex.Replace(cep, @"[^\d]", "");
            return cepNumeros.Length == 8;
        }

        private async Task<bool> ValidateEmailDuplicate(string email, CancellationToken cancellationToken) {
            var donor = await _unitOfWork.Donors.GetEmailAsync(email);
            return donor == null ? true : false;
        }
    }
}
