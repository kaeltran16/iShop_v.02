using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper.Internal;
using iShop.Data.Entities;
using iShop.Data.Models;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Repo.Data.Implementations
{
    public class CategoryRepository : DataRepositoryBase<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {        
        }

        public async Task<CategoryEntity> GetCategory(Guid id)
        {
            var spec = 
                new Specification<CategoryEntity>(predicate: c => c.Id == id, includes: null);

            return await GetSingleAsync(spec);
        }

        public async Task<IEnumerable<CategoryEntity>> GetCategories()
        {
            var spec = 
                new Specification<CategoryEntity>(predicate: null, includes: null); 

            return await GetAllAsync(spec);
        }


    }
}
