namespace HouseRentingSystem.Core.Services.Agents
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

        public async Task Create(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
            };

            await context.Agents.AddAsync(agent);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(string userId)
        {
            return context
                .Agents
                .Any(a => a.UserId == userId);
        }

        public async Task<bool> UserHasRents(string userId)
        {
            return context
                .Houses
                .Any(h => h.RenterId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            return context
                .Agents
                .Any(a => a.PhoneNumber == phoneNumber);
        }
    }
}
