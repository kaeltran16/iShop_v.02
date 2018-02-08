using System;
using iShop.Data.Base;

namespace iShop.Data.Models
{
    public class Inventory: KeyEntity, IModelBase
    {
        public Product Product { get; set; }
        public Supplier Supplier { get; set; }
        public int Stock { get; set; }
    }
}
