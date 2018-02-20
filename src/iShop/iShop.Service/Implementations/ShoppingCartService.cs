using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Data.Entities;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Interfaces;
using iShop.Service.Interfaces;

namespace iShop.Service.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IShoppingCartRepository _repository;

        public ShoppingCartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetRepository<IShoppingCartRepository>();
        }

        public async Task<ShoppingCartDto> CreateAsync(SavedShoppingCartDto shoppingCartDto)
        {
            var shoppingCart = _mapper.Map<SavedShoppingCartDto, ShoppingCart>(shoppingCartDto);

            await _repository.AddAsync(shoppingCart);

            foreach (var cartItem in shoppingCart.Carts)
            {
                shoppingCart.AddItem(cartItem.ProductId, cartItem.Quantity);
            }

            await _unitOfWork.CompleteAsync();

            return await Get(shoppingCart.Id);
        }

        public async Task<ShoppingCartDto> Get(Guid id)
        {
            var shoppingCart = await _repository.GetShoppingCart(id);
            return _mapper.Map<ShoppingCart, ShoppingCartDto>(shoppingCart);
        }

        public async Task<IEnumerable<ShoppingCartDto>> GetAll()
        {
            var shoppingCarts = await _repository.GetShoppingCarts();
            return _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartDto>>(shoppingCarts);
        }

        public async Task RemoveAsync(Guid shoppingCartId)
        {
            var shoppingCart = await _repository.GetShoppingCart(shoppingCartId, false);
            _repository.Remove(shoppingCart);
            await _unitOfWork.CompleteAsync();
        }


        public async Task<ShoppingCartDto> UpdateAsync(Guid shoppingCartId, SavedShoppingCartDto shoppingCartDto)
        {
            var shoppingCart = await _repository.GetShoppingCart(shoppingCartId);
            _mapper.Map(shoppingCartDto, shoppingCart);

            var addedCartItems =
                shoppingCartDto.Carts
                    .Where(cd =>
                        shoppingCart.Carts.All(oi => oi.ProductId != cd.ProductId))
                    .ToList();
            foreach (var cartItemDto in addedCartItems)
                shoppingCart.AddItem(cartItemDto.ProductId, cartItemDto.Quantity);

            var removedCartItems =
                shoppingCart.Carts.Where(oi => shoppingCartDto.Carts.Any(oir => oir.ProductId != oi.ProductId))
                    .ToList();
            foreach (var item in removedCartItems)
                shoppingCart.RemoveItem(item);

            await _unitOfWork.CompleteAsync();

            return await Get(shoppingCart.Id);
        }

    }
}
