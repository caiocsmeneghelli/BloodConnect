using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Enums;
using BloodConnect.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Infrastructure.Persistence.Repositories
{
    public class BloodStockRepository : IBloodStockRepository
    {
        private readonly BloodConnectContext _context;

        public BloodStockRepository(BloodConnectContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBloodStock(BloodStock bloodStock)
        {
            await _context.BloodStocks.AddAsync(bloodStock);
            return bloodStock.Id;
        }

        public async Task<List<BloodStock>> GetAllAsync()
        {
            return await _context.BloodStocks.ToListAsync();
        }

        public async Task<BloodStock?> GetByIdAsync(int id)
        {
            return await _context.BloodStocks.SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<BloodStock?> GetByTypeAndRhFactorAsync(BloodType type, RhFactor rhFactor)
        {
            return await _context.BloodStocks
                .Where(d => d.BloodType == type)
                .Where(d => d.RhFactor == rhFactor)
                .SingleOrDefaultAsync();
        }
    }
}
