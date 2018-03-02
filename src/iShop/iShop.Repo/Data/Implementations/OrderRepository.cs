using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Repo.Data.Implementations
{
    public class OrderRepository : DataRepositoryBase<Order>, IOrderRepository
    {

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetUserOrders(Guid userId, bool isIncludeRelative = true)
        {
            var includes = CreateInclusiveRelatives();

            ISpecification<Order> spec = isIncludeRelative
                ? new Specification<Order>(o => o.UserId == userId, includes)
                : new Specification<Order>(o => o.UserId == userId, null);

            return await Get(spec).ToListAsync();
        }

        public override Func<IQueryable<Order>, IIncludableQueryable<Order, object>> CreateInclusiveRelatives()
        {
            return
                order => order
                    .Include(o => o.OrderedItems)
                    .Include(o => o.Shipping)
                    .Include(o => o.Invoice)
                    .Include(o => o.User);
        }

        public override Dictionary<string, Expression<Func<Order, object>>> CreateQueryTerms()
        {
            var columnMap =
                new Dictionary<string, Expression<Func<Order, object>>>
                {
                    {"date", p => p.OrderedDate}
                };
            return columnMap;
        }
    }
}

