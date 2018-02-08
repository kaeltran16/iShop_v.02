using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IShoppingCartRepository : IDataRepository<ShoppingCartEntity>
    {
        Task<ShoppingCartEntity> GetShoppingCart(Guid id, bool isIncludeRelative = true);
        Task<IEnumerable<ShoppingCartEntity>> GetShoppingCarts(bool isIncludeRelative = true);
        Task<IEnumerable<ShoppingCartEntity>> GetUserShoppingCarts(Guid userId, bool isIncludeRelative = true);

    }
}
