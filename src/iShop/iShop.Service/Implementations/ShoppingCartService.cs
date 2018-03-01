using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Common.Exceptions;
using iShop.Common.Extensions;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Interfaces;
using iShop.Service.Commons;
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

        public async Task<IServiceResult> CreateAsync(SavedShoppingCartDto shoppingCartDto)
        {
            try
            {
                var shoppingCart = _mapper.Map<SavedShoppingCartDto, ShoppingCart>(shoppingCartDto);

                await _repository.AddAsync(shoppingCart);

                if (shoppingCart.Carts.Count > 0)
                {
                    foreach (var cartItem in shoppingCart.Carts)
                    {
                        shoppingCart.AddItem(cartItem.ProductId, cartItem.Quantity);
                    }
                }

                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(shoppingCart));
                }

                var result = await GetSingleAsync(shoppingCart.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }

        }

        public async Task<IServiceResult> GetSingleAsync(string id)
        {
            try
            {
                var shoppingCartId = id.ToGuid();

                var shoppingCart = await _repository.GetSingleAsync(shoppingCartId);
                if (shoppingCart == null)
                    throw new NotFoundException(nameof(shoppingCart), id);

                var shoppingCartDto = _mapper.Map<ShoppingCart, ShoppingCartDto>(shoppingCart);
                return new ServiceResult(payload: shoppingCartDto);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }

        }

        public async Task<IServiceResult> GetAllAsync(QueryObject queryTerm)
        {
            try
            {
                var shoppingCarts = queryTerm != null 
                    ? _repository.SortAndFilterAsync(queryTerm).Result.Items 
                    : await _repository.GetAllAsync();

                var shoppingCartsDto = _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartDto>>(shoppingCarts);
                return new ServiceResult(payload: shoppingCartsDto);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }

        }

        public async Task<IServiceResult> UpdateAsync(string id, SavedShoppingCartDto shoppingCartDto)
        {
            try
            {
                var shoppingCartId = id.ToGuid();
                var shoppingCart = await _repository.GetSingleAsync(shoppingCartId);
                _mapper.Map(shoppingCartDto, shoppingCart);

                AddOrRemoveCartItems(shoppingCart, shoppingCartDto);

                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(shoppingCart));
                }

                var result = await GetSingleAsync(shoppingCart.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }

        }

        public async Task<IServiceResult> RemoveAsync(string id)
        {
            try
            {
                var shoppingCartId = id.ToGuid();

                var shoppingCart = await _repository.GetSingleAsync(shoppingCartId, false);
                if (shoppingCart == null)
                    throw new NotFoundException(nameof(shoppingCart), shoppingCartId);

                _repository.Remove(shoppingCart);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(shoppingCart));
                }
                return new ServiceResult();
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }
        }


        private void AddOrRemoveCartItems(ShoppingCart shoppingCart, SavedShoppingCartDto shoppingCartDto)
        {
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
        }

    }
}
