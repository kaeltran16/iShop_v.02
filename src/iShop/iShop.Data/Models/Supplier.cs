using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;

namespace iShop.Data.Models
{
    public class Supplier : KeyEntity, IModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Inventory> Inventories { get; set; }

        public Supplier()
        {
            Inventories = new Collection<Inventory>();
        }
    }
}
