using iShop.Common.DTOs;
using iShop.Data.Entities;

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
