using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Repo.Mapping
{
    public class InventoryProfile:BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Inventory, InventoryDto>();
            CreateMap<InventoryDto, Inventory>()
                .ForMember(i => i.Id, opt => opt.Ignore());
        }
    }
}
