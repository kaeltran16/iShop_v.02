using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class OrderedItem: IEntityBase
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
