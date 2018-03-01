using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Service.Mapping
{
    public class OrderItemProfile:BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<OrderedItem, OrderedItemDto>();
            CreateMap<OrderedItemDto, OrderedItem>();
        }
    }
}
