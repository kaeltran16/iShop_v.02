using iShop.Data.Entities;
using iShop.Service.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class ProductsController : CrudController<Product, SavedProductDto, IProductService>
    {
        public ProductsController(IProductService service, ILogger<IProductService> logger)
            : base(service, logger)
        {
        }
    }
}