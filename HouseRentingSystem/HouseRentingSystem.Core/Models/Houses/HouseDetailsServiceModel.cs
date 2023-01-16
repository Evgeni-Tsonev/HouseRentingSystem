namespace HouseRentingSystem.Core.Models.Houses
{
    using HouseRentingSystem.Core.Models.Agents;

    public class HouseDetailsServiceModel : HouseServiceModel
    {
        public string Description { get; init; } = null!;

        public string Category { get; init; } = null!;

        public AgentServiceModel Agent { get; init; }
    }
}
