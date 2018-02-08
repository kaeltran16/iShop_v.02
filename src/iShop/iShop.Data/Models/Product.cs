using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;
using iShop.Data.Entities;

namespace iShop.Data.Models
{
    public class Product : KeyEntity, IModelBase
    {
        public Inventory Inventory { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Summary { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime AddedDate { get; set; }
        public ICollection<ImageEntity> Images { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

        public Product()
        {
            AddedDate = DateTime.Now;
            Images = new Collection<ImageEntity>();
            ProductCategories = new Collection<ProductCategory>();
        }
        
    }
}
