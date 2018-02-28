using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Common.Extensions;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iShop.Repo.Data.Implementations
{
    public class ProductRepository : DataRepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<Product> GetProduct(Guid id, bool isIncludeRelative = true)
        {
            ISpecification<Product> spec = isIncludeRelative
                ? new Specification<Product>(predicate: o => o.Id == id,
                    includes: source => source
                        .Include(p => p.ProductCategories)
                        .ThenInclude(c => c.Category)
                        .Include(p => p.Images)
                        .Include(p => p.Inventory)
                        .ThenInclude(i => i.Supplier))
                : new Specification<Product>(predicate: o => o.Id == id, includes: null);
            return await Get(spec).SingleOrDefaultAsync();
        }

       public async Task<IEnumerable<Product>> GetProducts(bool isIncludeRelative)
       {
           var spec = isIncludeRelative 
               ? CreateInclusiveRelatives() 
               : new Specification<Product>(null, null);

            return await Get(spec).ToListAsync();
        }

        public ISpecification<Product> CreateInclusiveRelatives()
        {
            var spec = new Specification<Product>(predicate: null,
                includes: source => source
                    .Include(p => p.ProductCategories)
                    .ThenInclude(c => c.Category)
                    .Include(p => p.Images)
                    .Include(p => p.Inventory)
                    .ThenInclude(i => i.Supplier));
            return spec;
        }


        public Dictionary<string, Expression<Func<Product, object>>> CreateQueryTerms()
        {
            var columnMap =
                new Dictionary<string, Expression<Func<Product, object>>>
                {
                    {"name", p => p.Name},
                    {"expired", p => p.ExpiredDate},
                    {"expire", p => p.ExpiredDate},
                    {"stock", p => p.Inventory.Stock},
                    {"price", p => p.Price}
                };
            return columnMap;
        }

        public async Task<QueryResult<Product>> GetAndFilterAsync(QueryObject queryTerm, bool isIncludeRelative = true)
        {
            var spec = isIncludeRelative 
                ? CreateInclusiveRelatives() 
                : new Specification<Product>(null, null);
            var query = Get(spec);

            var columnMap = CreateQueryTerms();
            query = query.ApplyPaging(queryTerm);
            query = query.ApplyOrdering(queryTerm, columnMap);
            var result =
                new QueryResult<Product>() { Items = await query.ToListAsync(), TotalItem = await query.CountAsync() };
            return result;
        }
    }
}
