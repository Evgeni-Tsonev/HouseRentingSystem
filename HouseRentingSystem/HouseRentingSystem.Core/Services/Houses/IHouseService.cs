namespace HouseRentingSystem.Core.Services.Houses
{
    using HouseRentingSystem.Core.Models.Houses;

    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();
    }
}
