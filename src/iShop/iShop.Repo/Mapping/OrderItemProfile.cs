using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Repo.Mapping
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
