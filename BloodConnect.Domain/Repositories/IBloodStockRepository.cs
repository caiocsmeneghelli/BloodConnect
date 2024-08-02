using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Domain.Repositories
{
    public interface IBloodStockRepository
    {
        Task<BloodStock?> GetByIdAsync(int id);
        Task<BloodStock?> GetByTypeAndRhFactorAsync(BloodType type, RhFactor rhFactor);
        Task<List<BloodStock>> GetAllAsync();
        Task<int> CreateBloodStock(BloodStock bloodStock);
        Task UpdateBloodStock(BloodStock bloodStock);
    }
}
