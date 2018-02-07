using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class ShippingDto
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime ShippingDate { get; set; }
        public ShippingStateDto ShippingState { get; set; }
      
        [Required]
        public double Charge { get; set; }
        [Required]
        [StringLength(50)]
        public string Ward { get; set; }
        [Required]
        [StringLength(50)]
        public string District { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [Required]
        public string UserName { get; set; }
        public Guid OrderId { get; set; }
        public ShippingDto()
        {
            ShippingState = ShippingStateDto.None;
        }
    }

    public enum ShippingStateDto
    {
        None = 0,
        Shipped = 1,
        Processing = 2,
    }
}
