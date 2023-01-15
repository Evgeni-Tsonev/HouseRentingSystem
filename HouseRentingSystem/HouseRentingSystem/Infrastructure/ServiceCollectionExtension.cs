namespace HouseRentingSystem.Infrastructure.DependencyInjection
{
    using HouseRentingSystem.Core.Models.Agents;
    using HouseRentingSystem.Core.Services.Houses;
    using HouseRentingSystem.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IAgentService, AgentService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

    }
}
