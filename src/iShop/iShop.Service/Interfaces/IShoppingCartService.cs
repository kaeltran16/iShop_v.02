using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.DTOs;

namespace iShop.Service.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> CreateAsync(SavedShoppingCartDto shoppingCartDto);
        Task<ShoppingCartDto> Get(Guid id);
        Task<IEnumerable<ShoppingCartDto>> GetAll();
        Task RemoveAsync(Guid shoppingCartId);
        Task<ShoppingCartDto> UpdateAsync(Guid shoppingCartId, SavedShoppingCartDto shoppingCartDto);
    }
}