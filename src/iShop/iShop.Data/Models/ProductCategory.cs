using System;
using iShop.Data.Base;

namespace iShop.Data.Models
{
    public class ProductCategory: IModelBase
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
