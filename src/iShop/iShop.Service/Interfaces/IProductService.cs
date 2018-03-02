using System;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Service.Base;
using iShop.Service.DTOs;

namespace iShop.Service.Interfaces
{
    public interface IProductService: ICrudServiceBase<SavedProductDto>, IServiceBase
    {
        void AddToInventory(Product product, Guid supplierId, int stock);
        void AddCategory(Product product, Guid categoryId);
        Task RemoveCategory(Guid productId, Guid categoryId);
    }
}
