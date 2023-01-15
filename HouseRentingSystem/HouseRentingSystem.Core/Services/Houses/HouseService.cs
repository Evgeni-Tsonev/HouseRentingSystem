namespace HouseRentingSystem.Core.Services.Houses
{
    using HouseRentingSystem.Core.Models.Houses;
    using HouseRentingSystem.Infrastructure;
    using System.Collections.Generic;

    public class HouseService : IHouseService
    {
        private readonly ApplicationDbContext context;

        public HouseService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<HouseIndexServiceModel> LastThreeHouses()
        {
            return context
                .Houses
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseIndexServiceModel()
                {
                    Id= h.Id,
                    Title= h.Title,
                    ImageUrl= h.ImageUrl,
                })
                .Take(3);
        }
    }
}
