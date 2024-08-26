using BloodConnect.Application.Commands.CreateDonation;
using BloodConnect.Domain.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;
        public CreateDonationCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(reg => reg.IdDonor)
                .NotEmpty()
                .WithMessage("O campo Id do doador não pode ser vazio.");

            RuleFor(reg => reg.QuantityMl)
                .InclusiveBetween(420, 470)
                .WithMessage("A quantidade de sangue doado deve ser maior que 420ml e menor que 470ml.");

            RuleFor(reg => reg)
                .MustAsync(ValidateBirthDonor)
                .WithMessage("Doador precisa ser maior de idade");

            RuleFor(reg => reg)
                .MustAsync(ValidateFrequencyOfDonation)
                .WithMessage("Doações feitas por homens devem ser feitas de 60 em 60 dias e de mulheres de 90 em 90 dias");
        }

        private async Task<bool> ValidateBirthDonor(CreateDonationCommand command, CancellationToken cancellationToken)
        {
            var donor = await _unitOfWork.Donors.GetAsync(command.IdDonor);
            if (donor == null) { return true; }
            

            DateTime birthDateLegal = DateTime.Now.AddYears(-18);
            return donor.BirthDate < birthDateLegal;
        }

        private async Task<bool> ValidateFrequencyOfDonation(CreateDonationCommand command, CancellationToken cancellationToken){
            var donor = await _unitOfWork.Donors.GetAsync(command.IdDonor);
            var lastDonation = await _unitOfWork.Donations.GetLastDonationByDonorAsync(command.IdDonor);
            if (donor is null || lastDonation is null) { return true; }
            
            if(donor.Genre == Domain.Enums.Genre.Male){
                return DateTime.Now >= lastDonation.CreatedAt.AddDays(60);
            }

            return DateTime.Now >= lastDonation.CreatedAt.AddDays(90);
        }
    }
}
