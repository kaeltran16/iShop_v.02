using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Repo.Mapping
{
    public class ApplicationUserProfile:BaseProfile
    {

        protected override void CreateMap()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserDto, ApplicationUser>();
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterDto>();
        }
    }
}
