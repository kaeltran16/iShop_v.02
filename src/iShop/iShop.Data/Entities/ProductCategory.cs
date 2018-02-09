using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class ProductCategory: IEntityBase 
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
