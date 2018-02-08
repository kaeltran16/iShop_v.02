using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Data.Models;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iShop.Repo.Data.Implementations
{
    public class ProductRepository : DataRepositoryBase<ProductEntity>, IProductRepository
    {

        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<ProductEntity> GetProduct(Guid id, bool isIncludeRelative = true)
        {
            ISpecification<ProductEntity> spec = isIncludeRelative
                ? new Specification<ProductEntity>(predicate: o => o.Id == id,
                    includes: source => source
                        .Include(p => p.ProductCategories)
                        .ThenInclude(c => c.Category)
                        .Include(p => p.Images)
                        .Include(p => p.Inventory)
                        .ThenInclude(i => i.Supplier))
                : new Specification<ProductEntity>(predicate: null, includes: null);
            return await GetSingleAsync(spec);
        }

        public async Task<IEnumerable<ProductEntity>> GetProducts(bool isIncludeRelative = true)
        {
            ISpecification<ProductEntity> spec = isIncludeRelative
                ? new Specification<ProductEntity>(predicate: null,
                    includes: source => source
                        .Include(p => p.ProductCategories)
                        .ThenInclude(c => c.Category)
                        .Include(p => p.Images)
                        .Include(p => p.Inventory)
                        .ThenInclude(i => i.Supplier))
                : new Specification<ProductEntity>(predicate: null, includes: null);

            return await GetAllAsync(spec);
        }
    }
}
