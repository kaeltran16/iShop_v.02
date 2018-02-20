using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.DTOs;

namespace iShop.Service.Interfaces
{
    public interface IShippingService
    {
        Task<ShippingDto> CreateAsync(ShippingDto shippingDto);
        Task<ShippingDto> Get(Guid id);
        Task<IEnumerable<ShippingDto>> GetAll();
        Task RemoveAsync(Guid shippingId);
        Task<ShippingDto> UpdateAsync(Guid shippingId, ShippingDto shippingDto);
    }
}
