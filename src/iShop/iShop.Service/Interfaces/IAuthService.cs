using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iShop.Service.Base;
using iShop.Service.Commons;

namespace iShop.Service.Interfaces
{
    public interface IAuthService: IServiceBase
    {
        Task<IServiceResult> Login(string username, string password);
    }
}
