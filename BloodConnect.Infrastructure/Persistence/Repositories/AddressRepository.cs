using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Infrastructure.Persistence.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly BloodConnectContext _context;

        public AddressRepository(BloodConnectContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
            return address.Id;
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _context.Addresses.SingleOrDefaultAsync(reg => reg.Id == id);
        }
    }
}
