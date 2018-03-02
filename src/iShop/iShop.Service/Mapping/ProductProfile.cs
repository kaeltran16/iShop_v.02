using System.Linq;
using iShop.Data.Entities;
using iShop.Service.DTOs;

namespace iShop.Service.Mapping
{
    public class ProductProfile : BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Product, SavedProductDto>();

            CreateMap<Product, ProductDto>()
                .ForMember(pr => pr.Categories,
                    opt => opt.MapFrom(p =>
                        p.ProductCategories.Select(pc => pc.Category)))
                .ForMember(pr => pr.Supplier, opt => opt.MapFrom(p => p.Inventory.Supplier))
                .ForAllMembers(opt => opt.Condition(
                    (src, des, srcMbr, desMbr) => (srcMbr != null)));

            CreateMap<ProductDto, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.ProductCategories, opt => opt.Ignore());
            CreateMap<SavedProductDto, Inventory>()
                .ForMember(i=>i.Id, opt=>opt.Ignore())
                .ForMember(i=>i.SupplierId, opt => opt.MapFrom(pdto => pdto.SupplierId))
                .ForMember(i=>i.Stock, opt => opt.MapFrom(pdto => pdto.Stock))
                .ForAllMembers(opt => opt.Condition(
                    (src, des, srcMbr, desMbr) => (srcMbr != null)));
            CreateMap<SavedProductDto, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.ProductCategories, opt => opt.Ignore())
                .ForMember(p => p.Inventory, opt=>opt.MapFrom(i=>i))
                .ForAllMembers(opt => opt.Condition(
                    (src, des, srcMbr, desMbr) => (srcMbr != null)));
        }
    }
}
