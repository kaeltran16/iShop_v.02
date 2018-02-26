using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class ApplicationUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }   
        public string City { get; set; }
        public string PhoneNumber { get; set; }
    }
}
