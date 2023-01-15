namespace HouseRentingSystem.Core.Services.Houses
{
    using HouseRentingSystem.Core.Models.Houses;

    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();

        Task<IEnumerable<HouseCategoryServiceModel>> AllCategories();

        Task<bool> CategoryExists(int categoryId);

        Task<int> Create(
            string title,
            string address,
            string description,
            string ImageUrl,
            decimal price,
            int categoryId,
            int agentId);


    }
}
