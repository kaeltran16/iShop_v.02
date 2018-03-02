using System.Linq;
using iShop.Data.Entities;
using iShop.Service.DTOs;

namespace iShop.Service.Mapping
{
    public class OrderProfile : BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Order, SavedOrderDto>();

            CreateMap<Order, OrderDto>()
                .ForMember(or => or.OrderedItems, opt => opt.MapFrom(p =>
                    p.OrderedItems.Select(pc => new OrderedItem { ProductId = pc.ProductId, Quantity = pc.Quantity })))
                .ForMember(or => or.Shipping, opt => opt.MapFrom(o => o.Shipping))
                .ForMember(or => or.Invoice, opt => opt.MapFrom(o => o.Invoice));

            CreateMap<Order, SavedOrderDto>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(d => d.OrderedItems, opt => opt.Ignore());

            CreateMap<SavedOrderDto, Order>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.OrderedItems, opt => opt.Ignore())
                .ForMember(o => o.Invoice, opt => opt.Ignore())
                .ForMember(o => o.Shipping, opt => opt.Ignore());
        }
    }
}
