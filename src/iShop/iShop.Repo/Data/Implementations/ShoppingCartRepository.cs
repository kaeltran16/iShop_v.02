using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Repo.Data.Implementations
{
    public class ShoppingCartRepository : DataRepositoryBase<ShoppingCart>, IShoppingCartRepository
    {

        public ShoppingCartRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ShoppingCart>> GetUserShoppingCarts(Guid userId, bool isIncludeRelative = true)
        {
            var includes = CreateInclusiveRelatives();
            var spec = isIncludeRelative
                ? new Specification<ShoppingCart>(p => p.UserId == userId, includes)
                : new Specification<ShoppingCart>(o => o.UserId == userId, null);

            return await Get(spec).ToListAsync();
        }

        public override Dictionary<string, Expression<Func<ShoppingCart, object>>> CreateQueryTerms()
        {
            var columnMap =
                new Dictionary<string, Expression<Func<ShoppingCart, object>>>
                {
                    {"date", p => p.PlacedDate}
                };
            return columnMap;
        }

        public override Func<IQueryable<ShoppingCart>, IIncludableQueryable<ShoppingCart, object>> CreateInclusiveRelatives()
        {
            return
                source => source
                    .Include(c => c.Carts)
                    .ThenInclude(p => p.Product)
                    .Include(u => u.User);
        }
    }
}
