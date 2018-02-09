using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IShippingRepository : IDataRepository<Shipping>
    {
        Task<Shipping> GetShipping(Guid id, bool isIncludeRelative = true);
        Task<IEnumerable<Shipping>> GetShippings(bool isIncludeRelative = true);
    }
}
