using System;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Common.Helpers;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class SuppliersController : CrudController<ISupplierService, SupplierDto>
    {
        public SuppliersController(ISupplierService service) : base(service)
        {
        }
    }
}
