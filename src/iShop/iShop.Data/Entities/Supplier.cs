using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class Supplier : KeyEntity, IEntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Inventory> Inventories { get; set; } = new Collection<Inventory>();

        public Supplier()
        {

        }
    }
}
