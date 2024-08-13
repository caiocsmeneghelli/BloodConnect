using BloodConnect.Domain.Repositories;
using BloodConnect.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly BloodConnectContext _context;

        public UnitOfWork(IDonorRepository donors, IDonationRepository donations,
            IAddressRepository addresses, IBloodStockRepository bloodStocks, BloodConnectContext context)
        {
            Donors = donors;
            Donations = donations;
            Addresses = addresses;
            BloodStocks = bloodStocks;
            _context = context;
        }

        public IDonorRepository Donors { get; }

        public IDonationRepository Donations {get;}

        public IAddressRepository Addresses {get;}

        public IBloodStockRepository BloodStocks {get;}

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async Task<int> CompletAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
