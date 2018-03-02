using iShop.Data.Entities;
using iShop.Service.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    /// <summary>
    /// This Controller is reposible for everything relative to Accounts
    /// </summary>
    [Route("/api/[controller]")]
    public class AccountsController : CrudController<ApplicationUser, RegisterDto, IAccountService>
    {
        public AccountsController(IAccountService accountService, ILogger<IAccountService> logger)
            : base(accountService, logger)
        {
            
        }
    }
}

