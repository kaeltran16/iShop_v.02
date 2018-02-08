using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;

namespace iShop.Data.Models
{
    public class Category : KeyEntity, IModelBase
    {
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Short { get; set; }

        public Category()
        {
        }
    }
}
