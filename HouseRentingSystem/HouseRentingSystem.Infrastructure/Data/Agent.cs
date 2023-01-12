namespace HouseRentingSystem.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Agent
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber  { get; set; } = null!;

        [Required]
        public string UserId  { get; set; } = null!;

        public User User  { get; set; } = null!;
    }
}