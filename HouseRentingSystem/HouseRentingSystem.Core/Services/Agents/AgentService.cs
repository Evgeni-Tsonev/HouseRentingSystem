namespace HouseRentingSystem.Core.Services.Agents
{
    using HouseRentingSystem.Infrastructure;
    using HouseRentingSystem.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

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
            return await context
                .Agents
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<int> GetAgentId(string userId)
        {
            var agent = await context
                .Agents
                .FirstOrDefaultAsync(a => a.UserId == userId);

            return agent.Id;
        }

        public async Task<bool> UserHasRents(string userId)
        {
            return await context
                .Houses
                .AnyAsync(h => h.RenterId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            return await context
                .Agents
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
