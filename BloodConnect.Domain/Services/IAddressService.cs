using BloodConnect.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Domain.Services
{
    public interface IAddressService
    {
        Task<AddressDto?> GetAddressByCEP(string cep);
    }
}
