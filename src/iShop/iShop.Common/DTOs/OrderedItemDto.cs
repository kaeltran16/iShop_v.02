using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class OrderedItemDto
    {
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}