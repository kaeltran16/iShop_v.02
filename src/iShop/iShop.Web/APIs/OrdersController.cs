using iShop.Common.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class OrdersController : CrudController<IOrderService, SavedOrderDto>
    {
        public OrdersController(IOrderService service) : base(service)
        {
        }
    }
}