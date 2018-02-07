using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IProductRepository : IDataRepository<Product>
    {
        Task<Product> GetProduct(Guid id, bool isIncludeRelative = true);
        Task<IEnumerable<Product>> GetProducts(bool isIncludeRelative = true);
    }
}
