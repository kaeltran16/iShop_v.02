using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class Image : KeyEntity, IEntityBase
    {
        public string FileName { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
