using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    }
}
