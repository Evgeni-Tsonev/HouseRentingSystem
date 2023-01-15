namespace HouseRentingSystem.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using static HouseRentingSystem.Infrastructure.Constants.DataConstants.Agent;

    public class Agent
    {
        public int Id { get; init; }

        [Required]
        [StringLength(PhoneNumberMaxLength)]
        public string PhoneNumber  { get; init; } = null!;

        [Required]
        public string UserId  { get; init; } = null!;

        public User User  { get; init; } = null!;
    }
}