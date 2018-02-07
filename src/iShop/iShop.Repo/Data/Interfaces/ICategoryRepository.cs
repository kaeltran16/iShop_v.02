using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface ICategoryRepository: IDataRepository<Category>
    {
        Task<Category> GetCategory(Guid id);
        Task<IEnumerable<Category>> GetCategories();
    }
}
