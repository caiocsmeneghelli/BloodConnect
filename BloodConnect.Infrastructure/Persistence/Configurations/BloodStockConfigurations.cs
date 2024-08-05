using BloodConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodConnect.Infrastructure.Persistence.Configurations
{
    public class BloodStockConfigurations : IEntityTypeConfiguration<BloodStock>
    {
        public void Configure(EntityTypeBuilder<BloodStock> builder)
        { 
            builder.HasKey(reg => reg.Id);
        }
    }
}
