using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class CategoryEntity : KeyEntity, IEntityBase
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Short { get; set; }
        public ICollection<ProductCategoryEntity> ProductCategories { get; set; }

        public CategoryEntity()
        {
            ProductCategories = new Collection<ProductCategoryEntity>();
        }
    }
}
