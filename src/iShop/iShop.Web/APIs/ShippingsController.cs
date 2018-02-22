using iShop.Common.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class ShippingsController : CrudController<IShippingService, ShippingDto>
    {
        public ShippingsController(IShippingService service) : base(service)
        {
        }
    }
}
