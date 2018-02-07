using System;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class Invoice : EntityBase
    {
        public DateTime InvoiceDate { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Invoice()
        {
            InvoiceDate = DateTime.Now;
        }
    }
}
