namespace HouseRentingSystem.Core.Models.Agents
{

    public interface IAgentService
    {
        bool ExistsById(string userId);

        bool UserWithPhoneNumberExists(string phoneNumber);

        bool UserHasRents(string userId);

        void Create(string userId, string phoneNumber);
    }
}
