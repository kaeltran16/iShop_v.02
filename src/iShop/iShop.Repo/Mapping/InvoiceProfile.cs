using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Repo.Mapping
{
    public class InvoiceProfile:BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>()
                .ForMember(sr => sr.Id, opt => opt.Ignore());

        }
    }
}
