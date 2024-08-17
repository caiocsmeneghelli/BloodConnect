using BloodConnect.Domain.Repositories;
using BloodConnect.Domain.Services;
using BloodConnect.Domain.UnitOfWork;
using BloodConnect.Infrastructure.Persistence.Repositories;
using BloodConnect.Infrastructure.Persistence.UnitOfWork;
using BloodConnect.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BloodConnect.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddRepositories()
                .AddUnitOfWork();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDonorRepository, DonorRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IBloodStockRepository, BloodStockRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            // FIX
            services.AddScoped<IAddressService, AddressService>();

            return services;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
