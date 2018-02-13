using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iShop.Common.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }      
        public string Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Summary { get; set; }
        public DateTime ExpiredDate { get; set; }
        public SupplierDto Supplier{ get; set; }
        public InventoryDto Inventory { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }
        public ICollection<ImageDto> Images { get; set; }

        public ProductDto()
        {
            Categories = new Collection<CategoryDto>();
            Images = new Collection<ImageDto>();
        }
    }
}
