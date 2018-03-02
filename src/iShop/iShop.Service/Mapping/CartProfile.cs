using iShop.Data.Entities;
using iShop.Service.DTOs;

namespace iShop.Service.Mapping
{
    public class CartProfile : BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Cart, CartDto>();
            CreateMap<CartDto, Cart>();
        }
    }
}
