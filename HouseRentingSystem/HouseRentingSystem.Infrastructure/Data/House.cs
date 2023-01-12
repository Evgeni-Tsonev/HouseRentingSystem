namespace HouseRentingSystem.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;

    public class House
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        public string Address { get; set; }

        [Required]
        [StringLength(500)]
        public string Description  { get; set; }

        [Required]
        public string ImageUrl   { get; set; }

        [Range(0, 2000)]
        public decimal PricePerMonth    { get; set; }

        public int CategoryId     { get; set; }

        public Category Category { get; set; }

        public int AgentId { get; set; }

        public Agent Agent { get; set; }

        public string RenterId { get; set; }
    }
}