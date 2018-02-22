using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class Cart: IEntityBase
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
