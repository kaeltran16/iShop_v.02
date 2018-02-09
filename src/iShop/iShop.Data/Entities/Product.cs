using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class Product : KeyEntity, IEntityBase
    {
        public Inventory Inventory { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Summary { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public ICollection<Image> Images { get; set; } = new Collection<Image>();
        public ICollection<ProductCategory> ProductCategories { get; set; } 
            = new Collection<ProductCategory>();
        public Collection<OrderedItem> OrderedItems { get; set; } 
            = new Collection<OrderedItem>();
        public ICollection<Cart> Carts { get; set; } = new Collection<Cart>();

        public Product()
        {
        }

        public async Task AddCategory(Guid categoryId)
        {
            
        }

    }
}
