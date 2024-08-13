using BloodConnect.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodConnect.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDonorRepository Donors { get; }
        IDonationRepository Donations { get; }
        IAddressRepository Addresses { get; }
        IBloodStockRepository BloodStocks { get; }

        Task<int> CompletAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}
