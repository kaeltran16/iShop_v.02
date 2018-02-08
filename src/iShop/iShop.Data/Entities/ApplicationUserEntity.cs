using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;
using Microsoft.AspNetCore.Identity;

namespace iShop.Data.Entities
{
    public class ApplicationUserEntity: IdentityUser<Guid>, IEntityBase
    {
        public string FirstName { get; set; }      
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public ICollection<ShoppingCartEntity> ShoppingCarts { get; set; }
        public ICollection<OrderEntity> Orders { get; set; }

        public ApplicationUserEntity()
        {
            CreatedDate = DateTime.Now;
            ShoppingCarts = new Collection<ShoppingCartEntity>();
            Orders = new Collection<OrderEntity>();
        }
    }
}
