namespace HouseRentingSystem.Infrastructure.DependencyInjection
{
    using HouseRentingSystem.Core.Services.Agents;
    using HouseRentingSystem.Core.Services.Houses;
    using HouseRentingSystem.Core.Services.Statistics;
    using HouseRentingSystem.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IStatisticsService, StatisticsService>();

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
