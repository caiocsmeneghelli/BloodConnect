using BloodConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Domain.Repositories
{
    public interface IAddressRepository
    {
        Task<int> CreateAsync(Address address);
        Task<int> GetByIdAsync(int id);
    }
}
