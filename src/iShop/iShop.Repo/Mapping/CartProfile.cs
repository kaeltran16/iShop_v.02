using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Repo.Mapping
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
