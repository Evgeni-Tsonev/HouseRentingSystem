namespace HouseRentingSystem.Models.Agents
{
    using System.ComponentModel.DataAnnotations;

    public class BecomeAgentFormModel
    {
        [Required]
        [StringLength(15, MinimumLength = 7)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
