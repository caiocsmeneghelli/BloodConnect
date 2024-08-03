using BloodConnect.Application.Commands.CreateDonor;
using BloodConnect.Domain.Enums;
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
        public CreateDonorCommandValidator()
        {
            RuleFor(reg => reg.FullName)
                .NotEmpty()
                .WithMessage("Campo Nome Completo não pode ser vazio.");

            RuleFor(reg => reg.Email)
               .NotEmpty()
               .WithMessage("Campo E-mail não pode ser vazio.");
            When(reg => !string.IsNullOrWhiteSpace(reg.Email), () =>
            {
                RuleFor(reg => reg.Email)
                    .Must(ValidateEmail)
                    .WithMessage("E-mail informado não válido.");
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
                .NotEmpty()
                .WithMessage("O campo Peso não pode ser vazio.");

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

            RuleFor(reg => reg.Street)
                .NotEmpty()
                .WithMessage("Campo Rua não pode ser vazio.");

            RuleFor(reg => reg.City)
                .NotEmpty()
                .WithMessage("Campo Cidade não pode ser vazio.");

            RuleFor(reg => reg.State)
                .NotEmpty()
                .WithMessage("Campo Estado não pode ser vazio.");

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

        private bool ValidateEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
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
    }
}
