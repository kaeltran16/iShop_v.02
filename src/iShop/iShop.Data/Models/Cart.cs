using System;
using iShop.Data.Base;

namespace iShop.Data.Models
{
    public class Cart: IModelBase
    {
        public Product Product { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
