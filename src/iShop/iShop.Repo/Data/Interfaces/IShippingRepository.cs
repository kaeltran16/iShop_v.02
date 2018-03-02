using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IShippingRepository : IDataRepository<Shipping>, IQueryableRepository<Shipping>
    {
    }
}
