using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Repo.Mapping
{
    public class ImageProfile:BaseProfile
    {
     

        protected override void CreateMap()
        {
            CreateMap<ImageEntity, ImageDto>();
            CreateMap<ImageDto, ImageEntity>();
        }
    }
}
