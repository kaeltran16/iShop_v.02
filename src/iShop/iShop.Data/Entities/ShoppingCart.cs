using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class ShoppingCart : EntityBase
    {
        public Guid? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public DateTime PlacedDate { get; set; }

        public ShoppingCart()
        {
            Carts = new Collection<Cart>();
            PlacedDate = DateTime.Now;
        }
    }
}
