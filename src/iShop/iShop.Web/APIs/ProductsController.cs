using iShop.Common.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class ProductsController : CrudController<IProductService, SavedProductDto>
    {
        public ProductsController(IProductService service) : base(service)
        {
        }
    }
}