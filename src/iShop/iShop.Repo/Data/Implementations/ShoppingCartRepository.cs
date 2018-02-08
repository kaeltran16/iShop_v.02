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
    public class ShoppingCartRepository : DataRepositoryBase<ShoppingCartEntity>, IShoppingCartRepository
    {

        public ShoppingCartRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ShoppingCartEntity>> GetUserShoppingCarts(Guid userId, bool isIncludeRelative = true)
        {
            ISpecification<ShoppingCartEntity> spec = isIncludeRelative
                ? new Specification<ShoppingCartEntity>(predicate: p => p.UserId == userId,
                    includes: source => source
                        .Include(c => c.Carts)
                        .ThenInclude(p => p.Product)
                        .Include(u => u.User))
                : new Specification<ShoppingCartEntity>(predicate: o => o.UserId == userId,
                    includes: null);

            return await GetAllAsync(spec);
        }

        public async Task<ShoppingCartEntity> GetShoppingCart(Guid id, bool isIncludeRelative = true)
        {
            ISpecification<ShoppingCartEntity> spec = isIncludeRelative
                ? new Specification<ShoppingCartEntity>(predicate: p => p.Id == id,
                    includes: source => source
                        .Include(c => c.Carts)
                        .ThenInclude(p => p.Product)
                        .Include(u => u.User))
                : new Specification<ShoppingCartEntity>(predicate: o => o.Id == id,
                    includes: null);

            return await GetSingleAsync(spec);
        }

        public async Task<IEnumerable<ShoppingCartEntity>> GetShoppingCarts(bool isIncludeRelative = true)
        {
            ISpecification<ShoppingCartEntity> spec = isIncludeRelative
                ? new Specification<ShoppingCartEntity>(predicate: null,
                    includes: source => source
                        .Include(c => c.Carts)
                        .ThenInclude(p => p.Product)
                        .Include(u => u.User))
                : new Specification<ShoppingCartEntity>(predicate: null,
                    includes: null);

            return await GetAllAsync(spec);
        }
    }
}
