using iShop.Data.Entities;
using iShop.Service.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class ShippingsController : CrudController<Shipping, ShippingDto, IShippingService>
    {
        public ShippingsController(IShippingService service, ILogger<IShippingService> logger) : base(service, logger)
        {
        }
    }
}
