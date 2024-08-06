using BloodConnect.Application.Validation;
using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Enums;
using BloodConnect.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Commands.CreateDonor
{
    public class CreateDonorCommandHandler : IRequestHandler<CreateDonorCommand, int>
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IAddressRepository _addressRepository;

        public CreateDonorCommandHandler(IDonorRepository donorRepository, IAddressRepository addressRepository)
        {
            _donorRepository = donorRepository;
            _addressRepository = addressRepository;
        }

        public async Task<int> Handle(CreateDonorCommand request, CancellationToken cancellationToken)
        {
            // Validate Command
            var validation = new CreateDonorCommandValidator();
            var validationResult = validation.Validate(request);
            if (!validationResult.IsValid)
            {
                List<string> errors = validationResult.Errors.Select(reg => reg.ErrorMessage).ToList();
                throw new Exception(errors.ToString());
            }


            Genre genreEnum;
            BloodType bloodTypeEnum;
            RhFactor rhFactorEnum;

            Enum.TryParse<Genre>(request.Genre, out genreEnum);
            Enum.TryParse<BloodType>(request.BloodType, out bloodTypeEnum);
            Enum.TryParse<RhFactor>(request.RhFactor, out rhFactorEnum);

            Donor donor = new Donor(request.FullName, request.Email, request.BirthDate.Value,
               genreEnum, request.Weight, bloodTypeEnum, rhFactorEnum);

            //Address address = new Address(request.Street, request.City, request.State, request.Cep);

            //donor.AddAddress(address);

            //await _addressRepository.CreateAsync(address);
            await _donorRepository.CreateAsync(donor);

            return donor.Id;
        }
    }
}
