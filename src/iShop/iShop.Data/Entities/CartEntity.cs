using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class CartEntity: IEntityBase
    {
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCartEntity ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
