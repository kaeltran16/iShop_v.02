using iShop.Data.Entities;
using iShop.Service.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class SuppliersController : CrudController<Supplier, SupplierDto, ISupplierService>
    {
        public SuppliersController(ISupplierService service, ILogger<ISupplierService> logger)
            : base(service, logger)
        {
        }
    }
}
