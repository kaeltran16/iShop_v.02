using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Repo.Mapping
{
    public class SupplierProfile : BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierDto, Supplier>()
                .ForMember(cr => cr.Id, opt => opt.Ignore())
                .ForMember(c => c.Inventories, opt => opt.Ignore());
        }
    }
}
