namespace HouseRentingSystem.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using static HouseRentingSystem.Infrastructure.Constants.DataConstants.Agent;

    public class Agent
    {
        public int Id { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength)]
        public string PhoneNumber  { get; set; } = null!;

        [Required]
        public string UserId  { get; set; } = null!;

        public User User  { get; set; } = null!;
    }
}