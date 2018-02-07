using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class SupplierDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
    }
}