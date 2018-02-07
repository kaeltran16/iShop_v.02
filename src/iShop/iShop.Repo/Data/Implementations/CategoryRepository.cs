using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;

namespace iShop.Repo.Data.Implementations
{
    public class CategoryRepository : DataRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {        
        }

        public async Task<Category> GetCategory(Guid id)
        {
            return await GetSingleAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await GetAllAsync();
        }


    }
}
