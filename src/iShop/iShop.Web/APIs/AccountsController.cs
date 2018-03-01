using iShop.Common.DTOs;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    /// <summary>
    /// This Controller is reposible for everything relative to Accounts
    /// </summary>
    [Route("/api/[controller]")]
    public class AccountsController : CrudController<IAccountService, RegisterDto>
    {
        public AccountsController(IAccountService accountService)
            : base(accountService)
        {
            
        }
    }
}

