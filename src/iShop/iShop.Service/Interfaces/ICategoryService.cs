using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> Get(Guid id);
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<CategoryDto> CreateAsync(CategoryDto categoryDto);
        Task<CategoryDto> UpdateAsync(Guid id, CategoryDto categoryDto);
        Task RemoveAsync(Guid id);
    }
}
