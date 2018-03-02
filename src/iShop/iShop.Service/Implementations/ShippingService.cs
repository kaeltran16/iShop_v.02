using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.Exceptions;
using iShop.Common.Extensions;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Interfaces;
using iShop.Service.Commons;
using iShop.Service.DTOs;
using iShop.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace iShop.Service.Implementations
{
    public class ShippingService : IShippingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ShippingService> _logger;
        private readonly IShippingRepository _repository;
        public ShippingService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ShippingService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _repository = _unitOfWork.GetRepository<IShippingRepository>();
        }

        public async Task<IServiceResult> CreateAsync(ShippingDto shippingDto)
        {
            try
            {
                var shipping = _mapper.Map<ShippingDto, Shipping>(shippingDto);

                await _repository.AddAsync(shipping);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(shipping));
                }

                _logger.LogInformation(
                    $"Added new shippng with id: {shipping.Id} for order with id: {shipping.OrderId}.");

                var result = await GetSingleAsync(shipping.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError($"Adding new shipping failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }

        }

        public async Task<IServiceResult> GetSingleAsync(string id)
        {
            try
            {
                var shippingId = id.ToGuid();

                var shipping = await _repository.GetSingleAsync(shippingId);
                if (shipping == null)
                    throw new NotFoundException(nameof(shipping), id);

                var shippingDto = _mapper.Map<Shipping, ShippingDto>(shipping);
                return new ServiceResult(payload: shippingDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting a shipping with id: {id} failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> GetAllAsync(QueryObject queryTerm)
        {
            try
            {
                var shippings = queryTerm != null 
                    ? _repository.SortAndFilterAsync(queryTerm).Result.Items 
                    : await _repository.GetAllAsync();
                var shippingsDto = _mapper.Map<IEnumerable<Shipping>, IEnumerable<ShippingDto>>(shippings);

                return new ServiceResult(payload: shippingsDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting all shippings failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> UpdateAsync(string id, ShippingDto shippingDto)
        {
            try
            {
                var shippingId = id.ToGuid();
                var shipping = await _repository.GetSingleAsync(shippingId);
                _mapper.Map(shippingDto, shipping);

                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(shipping));
                }
                _logger.LogInformation($"Updated {nameof(shipping)} with id: {shipping.Id} for order with id: {shipping.OrderId}");

                var result = await GetSingleAsync(shipping.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError($"Updating shipping with id: {id} failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> RemoveAsync(string id)
        {
            try
            {
                var shippingId = id.ToGuid();

                var shipping = await _repository.GetSingleAsync(shippingId, false);
                if (shipping == null)
                    throw new NotFoundException(nameof(shipping), shippingId);

                _repository.Remove(shipping);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(shipping));
                }
                _logger.LogInformation($"Delete {nameof(shipping)} with id: {shipping.Id}");

                return new ServiceResult();
            }
            catch (Exception e)
            {
                _logger.LogError($"Deleting shipping with id: {id} failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }
    }
}
