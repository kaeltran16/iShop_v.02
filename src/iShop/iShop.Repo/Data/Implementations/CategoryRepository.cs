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
            var spec = 
                new Specification<Category>(predicate: c => c.Id == id, includes: null);

            return await GetSingleAsync(spec);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var spec = 
                new Specification<Category>(predicate: null, includes: null); 

            return await GetAllAsync(spec);
        }


    }
}
