using iShop.Data.Entities;
using iShop.Service.DTOs;

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
