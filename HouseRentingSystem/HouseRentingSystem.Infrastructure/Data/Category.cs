﻿namespace HouseRentingSystem.Infrastructure.Data
{
    using System.ComponentModel.DataAnnotations;
    using static HouseRentingSystem.Infrastructure.Constants.DataConstants.Category;
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; init; } = null!;

        public IEnumerable<House> Houses { get; init; } = new List<House>();
    }
}
