using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IProductRepository : IDataRepository<ProductEntity>
    {
        Task<ProductEntity> GetProduct(Guid id, bool isIncludeRelative = true);
        Task<IEnumerable<ProductEntity>> GetProducts(bool isIncludeRelative = true);
    }
}
