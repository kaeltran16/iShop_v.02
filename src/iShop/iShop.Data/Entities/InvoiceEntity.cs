using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class InvoiceEntity : KeyEntity, IEntityBase
    {
        public DateTime InvoiceDate { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public InvoiceEntity()
        {
            InvoiceDate = DateTime.Now;
        }
    }
}
