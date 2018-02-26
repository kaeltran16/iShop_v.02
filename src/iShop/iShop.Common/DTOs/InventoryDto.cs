using System;
using System.ComponentModel.DataAnnotations;
using iShop.Common.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class InventoryDto
    {
        public Guid Id { get; set; }
        [Required]
        [GuidFormat(ErrorMessage = "The Product Id is missing or not in format.")]
        public Guid ProductId { get; set; }
        [Required]
        [GuidFormat(ErrorMessage = "The Supplier Id is missing or not in format.")]
        public Guid SupplierId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Stock must be a positive value and greater than 1.")]
        public int Stock { get; set; }
    }
}
