using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Data.Models;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface ICategoryRepository: IDataRepository<CategoryEntity>
    {
        Task<CategoryEntity> GetCategory(Guid id);
        Task<IEnumerable<CategoryEntity>> GetCategories();
    }
}
