﻿using BloodConnect.Application.Validation;
using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Enums;
using BloodConnect.Domain.Repositories;
using BloodConnect.Domain.Services;
using BloodConnect.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Application.Commands.CreateDonor
{
    public class CreateDonorCommandHandler : IRequestHandler<CreateDonorCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressService _addressService;

        public CreateDonorCommandHandler(IUnitOfWork unitOfWork, IAddressService addressService)
        {
            _unitOfWork = unitOfWork;
            _addressService = addressService;
        }


        public async Task<Result> Handle(CreateDonorCommand request, CancellationToken cancellationToken)
        {
            // Validate Command
            var validation = new CreateDonorCommandValidator(_unitOfWork);
            var validationResult = await validation.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure(request, validationResult.Errors
                    .Select(reg => reg.ErrorMessage).ToList());
            }

            Donor donor = new Donor(request.FullName, request.Email, request.BirthDate.Value,
               request.Genre, request.Weight, request.BloodType, request.RhFactor);

            var addressDto = await _addressService.GetAddressByCEP(request.Cep);

            await _unitOfWork.BeginTransactionAsync();
            if (addressDto is not null)
            {
                Address address = new Address(addressDto.Logradouro, addressDto.Localidade, addressDto.Uf, addressDto.Cep);
                donor.AddAddress(address);
                await _unitOfWork.Addresses.CreateAsync(address);
            }

            await _unitOfWork.Donors.CreateAsync(donor);

            await _unitOfWork.CommitAsync();

            return Result.Success(donor);
        }
    }
}
