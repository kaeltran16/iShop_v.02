﻿using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class ProductCategoryEntity: IEntityBase
    {
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}
