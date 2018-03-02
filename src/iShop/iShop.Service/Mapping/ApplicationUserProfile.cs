using iShop.Data.Entities;
using iShop.Service.DTOs;

namespace iShop.Service.Mapping
{
    public class ApplicationUserProfile : BaseProfile
    {

        protected override void CreateMap()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserDto, ApplicationUser>()
                .ForMember(g => g.Email, opt => opt.Ignore());
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterDto>();
        }
    }
}
