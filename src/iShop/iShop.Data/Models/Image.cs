using System;
using iShop.Data.Base;

namespace iShop.Data.Models
{
    public class Image : KeyEntity, IModelBase
    {
        public string FileName { get; set; }
        public Product Product { get; set; }
    }
}
