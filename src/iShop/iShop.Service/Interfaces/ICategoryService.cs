using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iShop.Repo.Data.Base;
using iShop.Service.Base;
using iShop.Service.DTOs;

namespace iShop.Service.Interfaces
{
    public interface ICategoryService: ICrudServiceBase<CategoryDto>, IServiceBase
    {
        
    }
}
