using iShop.Data.Entities;
using iShop.Service.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    /// <summary>
    /// This controller handles crud request for Cactegory
    /// </summary>
    [Route("/api/[controller]")]
    public class CategoriesController : CrudController<Category, CategoryDto, ICategoryService>
    {
        public CategoriesController(ICategoryService service, ILogger<ICategoryService> logger)
            : base(service, logger)
        {
        }
    }
}