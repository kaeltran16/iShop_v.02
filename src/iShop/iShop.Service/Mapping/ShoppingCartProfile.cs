using System.Linq;
using iShop.Data.Entities;
using iShop.Service.DTOs;

namespace iShop.Service.Mapping
{
    public class ShoppingCartProfile : BaseProfile
    {


        protected override void CreateMap()
        {
            CreateMap<ShoppingCart, SavedShoppingCartDto>();
            CreateMap<ShoppingCart, ShoppingCartDto>()
                .ForMember(or => or.Carts, opt => opt.MapFrom(p =>
                    p.Carts.Select(pc => new Cart {ProductId = pc.ProductId, Quantity = pc.Quantity})));

            CreateMap<ShoppingCartDto, ShoppingCart>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.Carts, opt => opt.Ignore());

            CreateMap<SavedShoppingCartDto, ShoppingCart>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.Carts, opt => opt.Ignore());
        }

    }
}
