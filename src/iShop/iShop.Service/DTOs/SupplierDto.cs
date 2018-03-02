using System;
using System.ComponentModel.DataAnnotations;
using iShop.Service.Base;

namespace iShop.Service.DTOs
{
    public class SupplierDto: ISavedBaseDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "City must not have longer than 100 characters.")]
        public string Name { get; set; }
        [StringLength(100, ErrorMessage = "City must not have longer than 100 characters.")]
        public string Description { get; set; }
    }
}