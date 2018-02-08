using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class InventoryEntity: KeyEntity, IEntityBase
    {
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public Guid SupplierId { get; set; }
        public SupplierEntity Supplier { get; set; }
        public int Stock { get; set; }
    }
}
