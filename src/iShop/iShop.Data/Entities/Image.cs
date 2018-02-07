using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class Image : EntityBase
    {
        public string FileName { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
