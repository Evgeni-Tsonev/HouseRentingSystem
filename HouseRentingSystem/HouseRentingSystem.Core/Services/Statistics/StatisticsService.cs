namespace HouseRentingSystem.Core.Services.Statistics
{
    using HouseRentingSystem.Core.Models.Statistics;
    using HouseRentingSystem.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext context;

        public StatisticsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<StatisticsServiceModel> Total()
        {
            var totalHouses = await context.Houses.CountAsync();
            var rentedHouses = await context
                .Houses
                .Where(h => h.RenterId != null)
                .CountAsync();

            return new StatisticsServiceModel()
            {
                TotalHouses = totalHouses,
                TotalRents = rentedHouses,
            };
        }
    }
}
