using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IOrderRepository : IDataRepository<Order>
    {
        Task<Order> GetOrder(Guid orderId, bool isIncludeRelative = true);
        Task<IEnumerable<Order>> GetOrders(bool isIncludeRelative = true);
        Task<IEnumerable<Order>> GetUserOrders(Guid userId, bool isIncludeRelative = true);
    }
}
