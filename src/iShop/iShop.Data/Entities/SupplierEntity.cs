using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class SupplierEntity : KeyEntity, IEntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<InventoryEntity> Inventories { get; set; }

        public SupplierEntity()
        {
            Inventories = new Collection<InventoryEntity>();
        }
    }
}
