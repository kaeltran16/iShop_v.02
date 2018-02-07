using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace iShop.Common.DTOs
{
    public class SavedOrderDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public ICollection<OrderedItemDto> OrderedItems { get; set; }
        public SavedOrderDto()
        {
            OrderedItems = new Collection<OrderedItemDto>();
        }
    }
}
