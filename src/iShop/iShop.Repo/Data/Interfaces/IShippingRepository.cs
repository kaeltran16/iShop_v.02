using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IShippingRepository : IDataRepository<ShippingEntity>
    {
        Task<ShippingEntity> GetShipping(Guid id, bool isIncludeRelative = true);
        Task<IEnumerable<ShippingEntity>> GetShippings(bool isIncludeRelative = true);
    }
}
