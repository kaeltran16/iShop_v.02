using System;
using iShop.Domain.Entities.Base;

namespace iShop.Domain.Entities.Entities
{
    public class Inventory: EntityBase
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int Stock { get; set; }
    }
}
