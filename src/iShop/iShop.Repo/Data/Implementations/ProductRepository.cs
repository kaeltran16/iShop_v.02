using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            return await GetSingleAsync(spec);
        }

        public async Task<IEnumerable<Product>> GetProducts(bool isIncludeRelative = true)
        {
            ISpecification<Product> spec = isIncludeRelative
                ? new Specification<Product>(predicate: null,
                    includes: source => source
                        .Include(p => p.ProductCategories)
                        .ThenInclude(c => c.Category)
                        .Include(p => p.Images)
                        .Include(p => p.Inventory)
                        .ThenInclude(i => i.Supplier))
                : new Specification<Product>(predicate: null, includes: null);

            return await GetAllAsync(spec);
        }
    }
}
