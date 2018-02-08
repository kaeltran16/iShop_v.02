using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class OrderedItemEntity: IEntityBase
    {
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public int Quantity { get; set; }
    }
}
