using System;
using System.ComponentModel.DataAnnotations;
using iShop.Common.Base;
using iShop.Common.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class ShippingDto: ISavedBaseDto
    {
        public Guid Id { get; set; }
        [Required]
        [FutureDate(ErrorMessage = "Shipping Date must be greater than today.")]
        public DateTime ShippingDate { get; set; }
        public ShippingStateDto ShippingState { get; set; } = ShippingStateDto.None;
        [Range(1, int.MaxValue, ErrorMessage = "Charge must be a positive value and greater than 1.")]
        public double Charge { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Ward must not have longer than 50 characters.")]
        public string Ward { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "District must not have longer than 50 characters.")]
        public string District { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "City must not have longer than 50 characters.")]
        public string City { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Phone number must not have longer than 50 characters.")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "User name must not have longer than 50 characters.")]
        public string UserName { get; set; }
        [GuidFormat(ErrorMessage = "The Order Id is missing or not in format.")]
        public Guid OrderId { get; set; }
    }

    public enum ShippingStateDto
    {
        None = 0,
        Shipped = 1,
        Processing = 2,
    }
}
