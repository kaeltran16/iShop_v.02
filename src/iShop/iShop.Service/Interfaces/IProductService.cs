using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Data.Entities;

namespace iShop.Service.Interfaces
{
    public interface IProductService
    {
        void AddToInventory(Product product, Guid supplierId, int stock);
        void AddCategory(Product product, Guid categoryId);
        Task RemoveCategory(Guid productId, Guid categoryId);
        Task<ProductDto> CreateAsync(SavedProductDto productDto);
        Task<ProductDto> Get(Guid id);
        Task<IEnumerable<ProductDto>> GetAll();
        Task RemoveAsync(Guid productId);
        Task<ProductDto> UpdateAsync(Guid productId, SavedProductDto productDto);
    }
}
