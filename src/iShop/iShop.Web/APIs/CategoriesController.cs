using System;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Common.Helpers;
using iShop.Service.Base;
using iShop.Service.Interfaces;
using iShop.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    /// <summary>
    /// This controller handles crud request for Cactegory
    /// </summary>
    [Route("/api/[controller]")]
    public class CategoriesController : CrudController<ICategoryService, CategoryDto>
    {
        public CategoriesController(ICategoryService service) : base(service)
        {
        }
    }
}