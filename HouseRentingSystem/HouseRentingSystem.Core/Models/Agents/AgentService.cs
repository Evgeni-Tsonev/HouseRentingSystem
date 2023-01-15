namespace HouseRentingSystem.Core.Models.Agents
{
    using HouseRentingSystem.Infrastructure;
    using HouseRentingSystem.Infrastructure.Data;

    public class AgentService : IAgentService
    {
        private readonly ApplicationDbContext context;

        public AgentService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
            };

            context.Agents.Add(agent);
            context.SaveChanges();
        }

        public bool ExistsById(string userId)
        {
            return context
                .Agents
                .Any(a => a.UserId == userId);
        }

        public bool UserHasRents(string userId)
        {
            return context
                .Houses
                .Any(h => h.RenterId == userId);
        }

        public bool UserWithPhoneNumberExists(string phoneNumber)
        {
            return context
                .Agents
                .Any(a => a.PhoneNumber == phoneNumber);
        }
    }
}
