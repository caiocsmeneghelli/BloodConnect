using BloodConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Domain.Repositories
{
    public interface IDonationRepository
    {
        Task<Donation?> GetByIdAsync(int idDonation);
        Task<List<Donation>> GetAll();
        Task<List<Donation>> GetAllByDonor(int idDonor);
        Task<int> Create(Donation donation);
    }
}
