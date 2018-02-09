using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IShoppingCartRepository : IDataRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetShoppingCart(Guid id, bool isIncludeRelative = true);
        Task<IEnumerable<ShoppingCart>> GetShoppingCarts(bool isIncludeRelative = true);
        Task<IEnumerable<ShoppingCart>> GetUserShoppingCarts(Guid userId, bool isIncludeRelative = true);

    }
}
