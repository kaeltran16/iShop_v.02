using AutoMapper;

namespace iShop.Repo.Mapping
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
