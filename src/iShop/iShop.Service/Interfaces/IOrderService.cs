using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Common.DTOs;

namespace iShop.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateAsync(SavedOrderDto orderDto);
        Task<OrderDto> Get(Guid id);
        Task<IEnumerable<OrderDto>> GetAll();
        Task RemoveAsync(Guid orderId);
        Task<OrderDto> UpdateAsync(Guid orderId, SavedOrderDto orderDto);
    }
}