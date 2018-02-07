using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            Expression<Func<Product, bool>> predicate = p => p.Id == id;

            return isIncludeRelative
                ? await GetSingleAsync(predicate,
                     includeProperties: src => src
                    .Include(p => p.ProductCategories)
                    .ThenInclude(c => c.Category)
                    .Include(p => p.Images)
                    .Include(p => p.Inventory)
                    .ThenInclude(i => i.Supplier))
                : await GetSingleAsync(predicate);
        }

        public async Task<IEnumerable<Product>> GetProducts(bool isIncludeRelative = true)
        {
            return isIncludeRelative
                ? await GetAllAsync(includeProperties: src => src.Include(p => p.ProductCategories)
                    .ThenInclude(c => c.Category)
                    .Include(p => p.Images)
                    .Include(p => p.Inventory)
                    .ThenInclude(i => i.Supplier))
                : await GetAllAsync();
        }
    }
}
