using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class Invoice : KeyEntity, IEntityBase
    {
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Invoice()
        {
            
        }
    }
}
