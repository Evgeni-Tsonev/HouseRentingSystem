namespace HouseRentingSystem.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using static HouseRentingSystem.Infrastructure.Constants.DataConstants.House;

    public class House
    {
        public int Id { get; init; }

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; init; } = null!;

        [Required]
        [StringLength(AddressMaxLength)]
        public string Address { get; init; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description  { get; init; } = null!;

        [Required]
        public string ImageUrl   { get; init; } = null!;

        [Range(PricePerMounthMinLength, PricePerMounthMaxLength)]
        public decimal PricePerMonth    { get; init; }

        public int CategoryId     { get; init; }
            
        public Category Category { get; init; } = null!;

        public int AgentId { get; init; }

        public Agent Agent { get; init; } = null!;

        public string? RenterId { get; init; }
    }
}