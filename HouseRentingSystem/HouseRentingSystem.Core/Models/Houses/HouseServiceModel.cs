namespace HouseRentingSystem.Core.Models.Houses
{
    using System.ComponentModel.DataAnnotations;

    public class HouseServiceModel
    {
        public int Id { get; init; }

        public string Title { get; init; } = null!;

        public string Address { get; init; } = null!;

        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; } = null!;

        [Display(Name = "Price Per Month")]
        public decimal PricePerMonth { get; init; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; init; }
    }
}
