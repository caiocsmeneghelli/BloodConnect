using BloodConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Domain.Repositories
{
    public interface IDonorRepository
    {
        Task<int> CreateAsync(Donor donor);
        Task<Donor?> GetAsync(int id);
        Task<List<Donor>> GetAllAsync();
    }
}
