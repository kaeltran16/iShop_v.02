using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class OrderEntity : KeyEntity, IEntityBase
    {
        public Guid? UserId { get; set; }
        public ApplicationUserEntity User { get; set; }

        public ShippingEntity Shipping { get; set; }

        public InvoiceEntity Invoice { get; set; }

        public ICollection<OrderedItemEntity> OrderedItems { get; set; }

        public DateTime OrderedDate { get; set; }

        public OrderEntity()
        {
            OrderedItems = new Collection<OrderedItemEntity>();
            OrderedDate = DateTime.Now;
        }
    }
}
