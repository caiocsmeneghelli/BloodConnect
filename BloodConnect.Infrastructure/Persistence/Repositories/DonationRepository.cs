using BloodConnect.Domain.Entities;
using BloodConnect.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodConnect.Infrastructure.Persistence.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private readonly BloodConnectContext _context;

        public DonationRepository(BloodConnectContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Donation donation)
        {
            await _context.Donations.AddAsync(donation);
            //await _context.SaveChangesAsync();
            return donation.Id;
        }

        public async Task<List<Donation>> GetAll()
        {
            return await _context.Donations.ToListAsync();
        }

        public async Task<List<Donation>> GetAllByDonor(int idDonor)
        {
            return await _context.Donations
                .Where(d => d.DonorId == idDonor)
                .ToListAsync();
        }

        public async Task<Donation?> GetByIdAsync(int idDonation)
        {
            return await _context.Donations.SingleOrDefaultAsync(d => d.Id == idDonation);
        }

        public async Task<Donation?> GetLastDonationByDonorAsync(int idDonor)
        {
            return await _context.Donations
                .OrderByDescending(d => d.CreatedAt)
                .FirstOrDefaultAsync(d => d.DonorId == idDonor);
        }
    }
}
