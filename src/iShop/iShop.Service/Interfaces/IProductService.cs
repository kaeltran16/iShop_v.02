using System;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Data.Entities;
using iShop.Service.Base;

namespace iShop.Service.Interfaces
{
    public interface IProductService: IServiceBase<SavedProductDto>
    {
        void AddToInventory(Product product, Guid supplierId, int stock);
        void AddCategory(Product product, Guid categoryId);
        Task RemoveCategory(Guid productId, Guid categoryId);
    }
}
