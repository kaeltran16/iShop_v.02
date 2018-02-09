using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;
using Microsoft.AspNetCore.Identity;

namespace iShop.Data.Entities
{
    public class ApplicationUser: IdentityUser<Guid>, IEntityBase
    {
        public string FirstName { get; set; }      
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new Collection<ShoppingCart>();
        public ICollection<Order> Orders { get; set; } = new Collection<Order>();

        private ApplicationUser()
        {

        }
    }
}
