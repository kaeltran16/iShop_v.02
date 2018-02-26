using System.ComponentModel.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class LoginDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Username must not have longer than 50 characters.")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Password must not have longer than 50 characters.")]
        public string Password { get; set; }
    }
}
