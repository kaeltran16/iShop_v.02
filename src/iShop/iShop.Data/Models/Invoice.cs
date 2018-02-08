using System;
using iShop.Data.Base;

namespace iShop.Data.Models
{
    public class Invoice : KeyEntity, IModelBase
    {
        public DateTime InvoiceDate { get; set; }
        public Order Order { get; set; }
        public Invoice()
        {
            InvoiceDate = DateTime.Now;
        }
    }
}
