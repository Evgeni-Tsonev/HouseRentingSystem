﻿namespace HouseRentingSystem.Core.Services.Houses
{
    using HouseRentingSystem.Core.Models.Enums;
    using HouseRentingSystem.Core.Models.Houses;

    public interface IHouseService
    {
        IEnumerable<HouseIndexServiceModel> LastThreeHouses();

        Task<IEnumerable<HouseCategoryServiceModel>> AllCategories();

        Task<IEnumerable<string>> AllCategoriesNames();

        Task<bool> CategoryExists(int categoryId);

        Task<int> Create(
            string title,
            string address,
            string description,
            string ImageUrl,
            decimal price,
            int categoryId,
            int agentId);

        Task<HouseQueryServiceModel> All(
            string category = null,
            string searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1);

        Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int agentId);

        Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId);

        Task<HouseDetailsServiceModel> HouseDetailsById(int id);

        Task<bool> Exists(int id);

        Task Edit(int houseId, 
            string title, 
            string address, 
            string description, 
            string imageUrl, 
            decimal price,
            int categoryId);

        Task<bool> HasAgentWithId(int houseId, string currentUserId);

        Task<int> GetHouseCategoryId(int houseId);

        Task Delete(int houseId);

        Task<bool> IsRented(int id);

        Task<bool> IsRentedByUserWithId(int houseId, string userId);

        Task Rent(int houseId, string userId);

        Task Leave(int houseId);
    }
}
