using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Service.Base;

namespace iShop.Service.Interfaces
{
    public interface IShoppingCartService: ICrudServiceBase<SavedShoppingCartDto>, IServiceBase
    {
       
    }
}