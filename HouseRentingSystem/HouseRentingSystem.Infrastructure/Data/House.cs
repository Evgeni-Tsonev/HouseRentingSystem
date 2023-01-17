namespace HouseRentingSystem.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using static HouseRentingSystem.Infrastructure.Constants.DataConstants.House;

    public class House
    {
        public int Id { get; init; }

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description  { get; set; } = null!;

        [Required]
        public string ImageUrl   { get; set; } = null!;

        [Range(PricePerMounthMinLength, PricePerMounthMaxLength)]
        public decimal PricePerMonth    { get; set; }

        public int CategoryId     { get; set; }
            
        public Category Category { get; set; } = null!;

        public int AgentId { get; set; }

        public Agent Agent { get; set; } = null!;

        public string? RenterId { get; set; }
    }
}