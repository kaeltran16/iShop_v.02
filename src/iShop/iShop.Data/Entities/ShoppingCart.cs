using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public void AddItem(Guid productId, int quantity)
        {
            var cartItem = Carts.SingleOrDefault(o => o.ProductId == productId && o.ShoppingCartId == Id);
            if (cartItem == null)
                cartItem = new Cart(){ProductId = productId, ShoppingCartId = Id, Quantity = quantity};
            else
                cartItem.Quantity += quantity;
            Carts.Add(cartItem);
        }

        public void RemoveItem(Cart item)
        {
            Carts.Remove(item);
        }
    }
}
