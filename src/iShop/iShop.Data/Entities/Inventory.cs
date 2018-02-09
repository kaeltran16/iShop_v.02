using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class Inventory: KeyEntity, IEntityBase
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int Stock { get; set; }
    }
}
