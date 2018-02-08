using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class ProductEntity : KeyEntity, IEntityBase
    {
        public InventoryEntity Inventory { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Summary { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime AddedDate { get; set; }
        public ICollection<ImageEntity> Images { get; set; }
        public ICollection<ProductCategoryEntity> ProductCategories { get; set; }
        public Collection<OrderedItemEntity> OrderedItems { get; set; }
        public ICollection<CartEntity> Carts { get; set; }

        public ProductEntity()
        {
            AddedDate = DateTime.Now;
            Images = new Collection<ImageEntity>();
            ProductCategories = new Collection<ProductCategoryEntity>();
            Carts = new Collection<CartEntity>();
            OrderedItems = new Collection<OrderedItemEntity>();
        }
        
    }
}
