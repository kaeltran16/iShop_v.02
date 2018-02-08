using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IOrderRepository : IDataRepository<OrderEntity>
    {
        Task<OrderEntity> GetOrder(Guid orderId, bool isIncludeRelative = true);
        Task<IEnumerable<OrderEntity>> GetOrders(bool isIncludeRelative = true);
        Task<IEnumerable<OrderEntity>> GetUserOrders(Guid userId, bool isIncludeRelative = true);
    }
}
