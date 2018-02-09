using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class ShoppingCart : KeyEntity, IEntityBase
    {
        public Guid? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Cart> Carts { get; set; } = new Collection<Cart>();
        public DateTime PlacedDate { get; set; } = DateTime.Now;

        public ShoppingCart()
        {
            
        }
    }
}
