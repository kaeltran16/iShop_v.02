using System.ComponentModel.DataAnnotations;
using iShop.Service.Base;

namespace iShop.Service.DTOs
{
    public class RegisterDto: ISavedBaseDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name must not have longer than 50 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name must not have longer than 50 characters.")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is not in format.")]
        [StringLength(50, ErrorMessage = "Email must not have longer than 50 characters.")]
        public string Email { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Password must not have longer than 50 characters.")]
        public string Password { get; set; }
        [StringLength(50, ErrorMessage = "District must not have longer than 50 characters.")]
        public string District { get; set; }
        [StringLength(50, ErrorMessage = "Ward must not have longer than 50 characters.")]
        public string Ward { get; set; }
        [StringLength(50, ErrorMessage = "City must not have longer than 50 characters.")]
        public string City { get; set; }
        [StringLength(50, ErrorMessage = "Phone number must not have longer than 50 characters.")]
        public string PhoneNumber { get; set; }

    }
}
