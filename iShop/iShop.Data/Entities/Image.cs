using System;
using iShop.Domain.Entities.Base;

namespace iShop.Domain.Entities.Entities
{
    public class Image : EntityBase
    {
        public string FileName { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
