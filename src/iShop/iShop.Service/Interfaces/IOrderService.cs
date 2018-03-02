using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Service.Base;
using iShop.Service.DTOs;

namespace iShop.Service.Interfaces
{
    public interface IOrderService: ICrudServiceBase<SavedOrderDto>, IServiceBase
    {
        
    }
}