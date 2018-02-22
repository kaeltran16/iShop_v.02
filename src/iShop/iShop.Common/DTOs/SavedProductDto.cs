using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using iShop.Common.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class SavedProductDto
    {
        public Guid Id { get; set; }
        [StringLength(20, ErrorMessage = "Sku must not have longer than 20 characters.")]
        public string Sku { get; set; } 
        [Required]
        [StringLength(50, ErrorMessage = "Name must not have longer than 50 characters.")]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive value and greater than 1.")]
        public double Price { get; set; }
        [StringLength(255, ErrorMessage = "Summary must not have longer than 255 characters.")]
        public string Summary { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Expired Date must be greater than today.")]
        public DateTime ExpiredDate { get; set; }   
        [GuidFormat(ErrorMessage = "The SupplierId is missing or not in format.")]
        public Guid SupplierId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Stock must be a positive value and greater than 1.")]
        public int Stock { get; set; }
        public ICollection<Guid> Categories { get; set; } = new Collection<Guid>();
    }
}
