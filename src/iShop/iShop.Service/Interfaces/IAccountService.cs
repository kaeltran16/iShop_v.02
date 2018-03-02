using System;
using System.Collections.Generic;
using System.Text;
using iShop.Service.Base;
using iShop.Service.DTOs;

namespace iShop.Service.Interfaces
{
    public interface IAccountService: ICrudServiceBase<RegisterDto>, IServiceBase
    {
    }
}
