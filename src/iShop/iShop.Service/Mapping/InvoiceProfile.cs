using iShop.Data.Entities;
using iShop.Service.DTOs;

namespace iShop.Service.Mapping
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
