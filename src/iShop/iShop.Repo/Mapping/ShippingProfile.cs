using iShop.Common.DTOs;
using iShop.Data.Entities;
using iShop.Data.Models;

namespace iShop.Repo.Mapping
{
    public class ShippingProfile:BaseProfile
    {
    

        protected override void CreateMap()
        {
            CreateMap<Shipping, ShippingDto>();

            CreateMap<ShippingDto, Shipping>()
                .ForMember(s => s.Id, opt => opt.Ignore())
                .ForMember(s => s.Order, opt => opt.Ignore());

        }
    }
}
