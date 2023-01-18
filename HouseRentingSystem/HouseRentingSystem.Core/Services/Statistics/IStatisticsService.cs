namespace HouseRentingSystem.Core.Services.Statistics
{
    using HouseRentingSystem.Core.Models.Statistics;

    public interface IStatisticsService
    {
        Task<StatisticsServiceModel> Total();
    }
}
