using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IProductRepository : IDataRepository<Product>, IQueryableRepository<Product>
    {
       
    }
}
