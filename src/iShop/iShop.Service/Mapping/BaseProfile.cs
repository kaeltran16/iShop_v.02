using AutoMapper;

namespace iShop.Service.Mapping
{
    public abstract class BaseProfile : Profile
    {
        protected BaseProfile()
        {
            CreateMap();
        }

        protected abstract void CreateMap();

       
    }
}
