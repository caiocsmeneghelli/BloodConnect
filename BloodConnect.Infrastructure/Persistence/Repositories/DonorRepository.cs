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
    public class DonorRepository : IDonorRepository
    {
        private readonly BloodConnectContext _context;

        public DonorRepository(BloodConnectContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Donor donor)
        {
            await _context.Donors.AddAsync(donor);
            await _context.SaveChangesAsync();
            return donor.Id;
        }

        public async Task<List<Donor>> GetAllAsync()
        {
            return await _context.Donors
                .Include(d => d.Address)
                .ToListAsync();
        }

        public async Task<Donor?> GetAsync(int id)
        {
            return await _context.Donors.SingleOrDefaultAsync(d => d.Id == id);
        }
    }
}
