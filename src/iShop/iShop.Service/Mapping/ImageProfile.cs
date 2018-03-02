using iShop.Data.Entities;
using iShop.Service.DTOs;

namespace iShop.Service.Mapping
{
    public class ImageProfile:BaseProfile
    {
     

        protected override void CreateMap()
        {
            CreateMap<Image, ImageDto>();
            CreateMap<ImageDto, Image>();
        }
    }
}
