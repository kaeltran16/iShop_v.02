using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.DTOs;

namespace iShop.Service.Interfaces
{
    public interface ISupplierService
    {
        Task<SupplierDto> Get(Guid id);
        Task<IEnumerable<SupplierDto>> GetAll();
        Task<SupplierDto> CreateAsync(SupplierDto supplierDto);
        Task<SupplierDto> UpdateAsync(Guid id, SupplierDto supplierDto);
        Task RemoveAsync(Guid id);
    }
}
