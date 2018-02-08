using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class ImageEntity : KeyEntity, IEntityBase
    {
        public string FileName { get; set; }
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}
