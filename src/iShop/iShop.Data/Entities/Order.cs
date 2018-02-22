using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using iShop.Data.Base;

namespace iShop.Data.Entities
{
    public class Order : KeyEntity, IEntityBase
    {
        public Guid? UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Shipping Shipping { get; set; }

        public Invoice Invoice { get; set; }

        public ICollection<OrderedItem> OrderedItems { get; set; }
            = new Collection<OrderedItem>();

        public DateTime OrderedDate { get; set; } =  DateTime.Now;

        public Order()
        {
            
        }

        public void AddItem(Guid productId, int quantity)
        {
            var orderItem = OrderedItems.SingleOrDefault(o => o.ProductId == productId && o.OrderId == Id);
            if (orderItem == null)
            orderItem = new OrderedItem(){ProductId = productId, OrderId = Id, Quantity = quantity};
            else
                orderItem.Quantity += quantity;
            OrderedItems.Add(orderItem);
        }

        public void RemoveItem(OrderedItem item)
        {
            OrderedItems.Remove(item);
        }
    }
}
