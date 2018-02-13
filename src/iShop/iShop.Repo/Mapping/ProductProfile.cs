using System.Linq;
using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Repo.Mapping
{
    public class ProductProfile: BaseProfile
    {
        protected override void CreateMap()
        {
            CreateMap<Product, SavedProductDto>();

            CreateMap<Product, ProductDto>()
                .ForMember(pr => pr.Categories,
                    opt => opt.MapFrom(p =>
                        p.ProductCategories.Select(pc => pc.Category)))
                .ForMember(pr => pr.Inventory, opt => opt.MapFrom(p => p.Inventory))
                .ForMember(pr => pr.Supplier, opt => opt.MapFrom(p => p.Inventory.Supplier));


            CreateMap<ProductDto, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(d => d.ProductCategories, opt => opt.Ignore());


            CreateMap<SavedProductDto, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.ProductCategories, opt => opt.Ignore())
                .ForMember(p => p.Inventory, opt => opt.Ignore());


        }
    }
}
