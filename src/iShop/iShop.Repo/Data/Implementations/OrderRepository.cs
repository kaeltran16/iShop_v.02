using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iShop.Repo.Data.Implementations
{
    public class OrderRepository : DataRepositoryBase<Order>, IOrderRepository
    {

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrders(bool isIncludeRelative = true)
        {
            ISpecification<Order> spec = isIncludeRelative
                ? new Specification<Order>(predicate: null,
                    includes: source => source
                        .Include(o => o.OrderedItems)
                        .Include(o => o.Shipping)
                        .Include(o => o.Invoice)
                        .Include(o => o.User))

                : new Specification<Order>(predicate: null,
                    includes: null);

            return await GetAllAsync(spec);
        }

        public async Task<IEnumerable<Order>> GetUserOrders(Guid userId, bool isIncludeRelative = true)
        {
            ISpecification<Order> spec = isIncludeRelative
                ? new Specification<Order>(predicate: o => o.UserId == userId,
                    includes: source => source
                        .Include(o => o.OrderedItems)
                        .Include(o => o.Shipping)
                        .Include(o => o.Invoice)
                        .Include(o => o.User))
                : new Specification<Order>(predicate: o => o.UserId == userId,
                    includes: null);

            return await GetAllAsync(spec);
        }

        public async Task<Order> GetOrder(Guid orderId, bool isIncludeRelative = true)
        {
            ISpecification<Order> spec = isIncludeRelative
                ? new Specification<Order>(predicate: o => o.Id == orderId,
                    includes: source => source
                        .Include(o => o.OrderedItems)
                        .Include(o => o.Shipping)
                        .Include(o => o.Invoice)
                        .Include(o => o.User))

                : new Specification<Order>(predicate: o => o.Id == orderId,
                    includes: null);

            return await GetSingleAsync(spec);
        }
    }
}

