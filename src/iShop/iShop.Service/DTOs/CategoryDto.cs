using System;
using System.ComponentModel.DataAnnotations;
using iShop.Service.Base;

namespace iShop.Service.DTOs
{
    public class CategoryDto: ISavedBaseDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name must not have longer than 100 characters.")]
        public string Name { get; set; }
        [StringLength(255, ErrorMessage = "Detail must not have longer than 255 characters.")]
        public string Detail { get; set; }
        [StringLength(50, ErrorMessage = "Short must not have longer than 50 characters.")]
        public string Short { get; set; }
    }
}
