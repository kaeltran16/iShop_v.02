using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using iShop.Common.Exceptions;
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

        private Product()
        {
        }

        public void AddCategory(Guid categoryId)
        {
            try
            {
                var productCategory = new ProductCategory() { ProductId = Id, CategoryId = categoryId };
                ProductCategories.Add(productCategory);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void RemoveCategory(Guid categoryId)
        {
            var removedCategory =
                ProductCategories.SingleOrDefault(g => g.CategoryId == categoryId && g.ProductId == Id);
            if (removedCategory == null)
                throw new NotFoundException(nameof(categoryId), categoryId);
            ProductCategories.Remove(removedCategory);
        }

        public void AddToInventory(int stock, Guid supplierId)
        {
            try
            {
                if (Inventory == null)
                    Inventory =
                        new Inventory() { ProductId = Id, SupplierId = supplierId, Stock = stock };
                else
                    Inventory.Stock += stock;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
