using BloodConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BloodConnect.Infrastructure.Persistence
{
    public class BloodConnectContext : DbContext
    {
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<BloodStock> BloodStocks { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public BloodConnectContext(DbContextOptions<BloodConnectContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
