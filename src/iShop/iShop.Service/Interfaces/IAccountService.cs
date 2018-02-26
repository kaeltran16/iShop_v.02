using System;
using System.Collections.Generic;
using System.Text;
using iShop.Common.DTOs;
using iShop.Service.Base;

namespace iShop.Service.Interfaces
{
    public interface IAccountService: ICrudServiceBase<RegisterDto>, IServiceBase
    {
    }
}
