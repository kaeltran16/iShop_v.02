using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IOrderRepository : IDataRepository<Order>, IQueryableRepository<Order>
    {
        Task<IEnumerable<Order>> GetUserOrders(Guid userId, bool isIncludeRelative = true);
    }
}
