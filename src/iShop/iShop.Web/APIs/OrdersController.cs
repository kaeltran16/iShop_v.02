using iShop.Data.Entities;
using iShop.Service.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class OrdersController : CrudController<Order, SavedOrderDto, IOrderService>
    {
        public OrdersController(IOrderService service, ILogger<IOrderService> logger) 
            : base(service, logger)
        {
        }
    }
}