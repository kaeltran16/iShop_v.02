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
    public class OrderRepository : DataRepositoryBase<OrderEntity>, IOrderRepository
    {

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderEntity>> GetOrders(bool isIncludeRelative = true)
        {
            ISpecification<OrderEntity> spec = isIncludeRelative
                ? new Specification<OrderEntity>(predicate: null,
                    includes: source => source
                        .Include(o => o.OrderedItems)
                        .Include(o => o.Shipping)
                        .Include(o => o.Invoice)
                        .Include(o => o.User))

                : new Specification<OrderEntity>(predicate: null,
                    includes: null);

            return await GetAllAsync(spec);
        }

        public async Task<IEnumerable<OrderEntity>> GetUserOrders(Guid userId, bool isIncludeRelative = true)
        {
            ISpecification<OrderEntity> spec = isIncludeRelative
                ? new Specification<OrderEntity>(predicate: o => o.UserId == userId,
                    includes: source => source
                        .Include(o => o.OrderedItems)
                        .Include(o => o.Shipping)
                        .Include(o => o.Invoice)
                        .Include(o => o.User))
                : new Specification<OrderEntity>(predicate: o => o.UserId == userId,
                    includes: null);

            return await GetAllAsync(spec);
        }

        public async Task<OrderEntity> GetOrder(Guid orderId, bool isIncludeRelative = true)
        {
            ISpecification<OrderEntity> spec = isIncludeRelative
                ? new Specification<OrderEntity>(predicate: o => o.Id == orderId,
                    includes: source => source
                        .Include(o => o.OrderedItems)
                        .Include(o => o.Shipping)
                        .Include(o => o.Invoice)
                        .Include(o => o.User))

                : new Specification<OrderEntity>(predicate: o => o.Id == orderId,
                    includes: null);

            return await GetSingleAsync(spec);
        }
    }
}

