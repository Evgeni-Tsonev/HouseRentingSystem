namespace HouseRentingSystem.Core.Services.Houses
{
    using HouseRentingSystem.Core.Models.Agents;
    using HouseRentingSystem.Core.Models.Enums;
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

        public async Task<HouseQueryServiceModel> All(
            string category = null,
            string searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1)
        {
            var housesQuery = context.Houses.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                housesQuery = housesQuery
                    .Where(c => c.Category.Name == category);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                housesQuery = housesQuery
                    .Where(h =>
                        h.Title.ToLower().Contains(searchTerm.ToLower()) ||
                        h.Address.ToLower().Contains(searchTerm.ToLower()) ||
                        h.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            housesQuery = sorting switch
            {
                HouseSorting.Price => housesQuery
                   .OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesQuery
                   .OrderBy(h => h.RenterId != null)
                   .ThenByDescending(h => h.Id),
                _ => housesQuery.OrderByDescending(h => h.Id)
            };

            var houses = await housesQuery
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(h => new HouseServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null,
                })
                .ToListAsync();

            var totalHousesCount = housesQuery.Count();

            return new HouseQueryServiceModel()
            {
                TotalHousesCount = totalHousesCount,
                Houses = houses,
            };
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

        public async Task<IEnumerable<string>> AllCategoriesNames()
        {
            return await context
                .Categories
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int agentId)
        {
            var houses = await context
                .Houses
                .Where(h => h.AgentId == agentId)
                .ToListAsync();

            return ProjectToModel(houses);
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId)
        {
            var houses = await context
                .Houses
                .Where(h => h.RenterId == userId)
                .ToListAsync();

            return ProjectToModel(houses);
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

        public async Task Delete(int houseId)
        {
            var house = await context.Houses.FindAsync(houseId);

            context.Houses.Remove(house);
            await context.SaveChangesAsync();
        }

        public async Task Edit(int houseId,
            string title,
            string address,
            string description,
            string imageUrl,
            decimal price,
            int categoryId)
        {
            var house = await context.Houses.FindAsync(houseId);

            house.Title = title;
            house.Address = address;
            house.Description = description;
            house.ImageUrl = imageUrl;
            house.PricePerMonth = price;
            house.CategoryId = categoryId;

            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await context
                .Houses
                .AnyAsync(h => h.Id == id);
        }

        public async Task<int> GetHouseCategoryId(int houseId)
        {
            var house = await context.Houses.FindAsync(houseId);
            return house.CategoryId;
        }

        public async Task<bool> HasAgentWithId(int houseId, string currentUserId)
        {
            var house = await context.Houses.FindAsync(houseId);
            var agent = await context.Agents.FirstOrDefaultAsync(a => a.UserId == currentUserId);

            if (agent == null)
            {
                return false;
            }

            if (agent.UserId != currentUserId)
            {
                return false;
            }

            return true;
        }

        public async Task<HouseDetailsServiceModel> HouseDetailsById(int id)
        {
            return await context
                .Houses
                .Where(h => h.Id == id)
                .Select(h => new HouseDetailsServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null,
                    Category = h.Category.Name,
                    Agent = new AgentServiceModel()
                    {
                        PhoneNumber = h.Agent.PhoneNumber,
                        Email = h.Agent.User.Email,
                    }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsRented(int id)
        {
            var house = await context.Houses.FindAsync(id);
            return house.RenterId != null;
        }

        public async Task<bool> IsRentedByUserWithId(int houseId, string userId)
        {
            var house = await context.Houses.FindAsync(houseId);

            if (house == null)
            {
                return false;
            }

            if (house.RenterId != userId)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<HouseIndexServiceModel> LastThreeHouses()
        {
            return context
                .Houses
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseIndexServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    ImageUrl = h.ImageUrl,
                })
                .Take(3);
        }

        public async Task Leave(int houseId)
        {
            var house = await context.Houses.FindAsync(houseId);

            house.RenterId = null;
            await context.SaveChangesAsync();
        }

        public async Task Rent(int houseId, string userId)
        {
            var house = await context.Houses.FindAsync(houseId);

            house.RenterId = userId;
            await context.SaveChangesAsync();
        }

        private IEnumerable<HouseServiceModel> ProjectToModel(List<House> houses)
        {
            var result = houses.Select(h => new HouseServiceModel()
            {
                Id = h.Id,
                Title = h.Title,
                Address = h.Address,
                ImageUrl = h.ImageUrl,
                PricePerMonth = h.PricePerMonth,
                IsRented = h.RenterId != null,
            })
                .ToList();

            return result;
        }
    }
}