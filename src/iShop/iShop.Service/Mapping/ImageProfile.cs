using iShop.Common.DTOs;
using iShop.Data.Entities;

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
