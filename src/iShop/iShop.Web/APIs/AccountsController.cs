using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

