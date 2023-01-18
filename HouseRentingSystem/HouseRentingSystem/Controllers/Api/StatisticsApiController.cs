namespace HouseRentingSystem.Controllers.Api
{
    using HouseRentingSystem.Core.Models.Statistics;
    using HouseRentingSystem.Core.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsApiController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<StatisticsServiceModel> GetStatistics()
        {
            return await statisticsService.Total();
        }
    }
}
