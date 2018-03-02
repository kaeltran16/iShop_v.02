using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Repo.Data.Implementations
{
    public class CategoryRepository : DataRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override Func<IQueryable<Category>, IIncludableQueryable<Category, object>> CreateInclusiveRelatives()
        {
            return null;
        }

        public override Dictionary<string, Expression<Func<Category, object>>> CreateQueryTerms()
        {
            var columnMap =
                new Dictionary<string, Expression<Func<Category, object>>>
                {
                    {"name", p => p.Name}
                };
            return columnMap;
        }
    }
}
