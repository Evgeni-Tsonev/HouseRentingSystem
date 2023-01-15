namespace HouseRentingSystem.Core.Services.Houses
{
    using HouseRentingSystem.Core.Models.Houses;
    using HouseRentingSystem.Infrastructure;
    using HouseRentingSystem.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class HouseService : IHouseService
    {
        private readonly ApplicationDbContext context;

        public HouseService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategories()
        {
            return await context
                .Categories
                .Select(c => new HouseCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await context
                .Categories
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> Create(
            string title,
            string address,
            string description,
            string ImageUrl,
            decimal price,
            int categoryId,
            int agentId)
        {
            var house = new House()
            {
                Title = title,
                Address = address,
                Description = description,
                ImageUrl = ImageUrl,
                PricePerMonth = price,
                AgentId = agentId,
                CategoryId = categoryId,
            };

            await context.Houses.AddAsync(house);
            await context.SaveChangesAsync();

            return house.Id;
        }

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses()
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
