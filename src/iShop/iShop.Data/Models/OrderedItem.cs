using System;

namespace iShop.Data.Models
{
    public class OrderedItem
    {
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
