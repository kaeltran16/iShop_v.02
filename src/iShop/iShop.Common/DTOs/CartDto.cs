using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class CartDto
    {
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
