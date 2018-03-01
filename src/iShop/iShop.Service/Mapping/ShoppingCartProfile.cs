using System.Linq;
using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Service.Mapping
{
    public class ShoppingCartProfile : BaseProfile
    {


        protected override void CreateMap()
        {
            CreateMap<ShoppingCart, SavedShoppingCartDto>();
            CreateMap<ShoppingCart, ShoppingCartDto>()
                .ForMember(or => or.Carts, opt => opt.MapFrom(p =>
                    Enumerable.Select<Cart, Cart>(p.Carts, pc => new Cart() {ProductId = pc.ProductId, Quantity = pc.Quantity})));

            CreateMap<ShoppingCartDto, ShoppingCart>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.Carts, opt => opt.Ignore());

            CreateMap<SavedShoppingCartDto, ShoppingCart>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.Carts, opt => opt.MapFrom(c => c.Carts))
                .AfterMap((sr, s) =>
                {
                    var addedCarts = Enumerable.Where<CartDto>(sr.Carts, cr => Enumerable.All<Cart>(s.Carts, c => c.ProductId != cr.ProductId))
                        .Select(cr => new Cart() { ProductId = cr.ProductId, Quantity = cr.Quantity, ShoppingCartId = sr.Id }).ToList();
                    foreach (var c in addedCarts)
                        s.Carts.Add(c);

                    var removedCartItems =
                        Enumerable.Where<Cart>(s.Carts, oi => Enumerable.Any<CartDto>(sr.Carts, oir=>oir.ProductId!=oi.ProductId)).ToList();
                    foreach (var oi in removedCartItems)
                        s.Carts.Remove(oi);
                });
        }

    }
}
