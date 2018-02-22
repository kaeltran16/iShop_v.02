using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Common.Exceptions;
using iShop.Common.Extensions;
using iShop.Data.Entities;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Interfaces;
using iShop.Service.Commons;
using iShop.Service.Interfaces;

namespace iShop.Service.Implementations
{
    public class ShippingService : IShippingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IShippingRepository _repository;
        public ShippingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                
                var result = await GetSingleAsync(shipping.Id.ToString());
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
                var shippingId = id.ToGuid();

                var shipping = await _repository.GetShipping(shippingId);
                if (shipping == null)
                    throw new NotFoundException(nameof(shipping), id);

                var shippingDto = _mapper.Map<Shipping, ShippingDto>(shipping);
                return new ServiceResult(payload: shippingDto);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }            
        }

        public async Task<IServiceResult> GetAllAsync()
        {
            try
            {
                var shippings = await _repository.GetShippings();
                var shippingsDto = _mapper.Map<IEnumerable<Shipping>, IEnumerable<ShippingDto>>(shippings);

                return new ServiceResult(payload: shippingsDto);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }         
        }

        public async Task<IServiceResult> UpdateAsync(string id, ShippingDto shippingDto)
        {
            try
            {
                var shippingId = id.ToGuid();
                var shipping = await _repository.GetShipping(shippingId);
                _mapper.Map(shippingDto, shipping);

                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(shipping));
                }

                var result = await GetSingleAsync(shipping.Id.ToString());
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
                var shippingId = id.ToGuid();

                var shipping = await _repository.GetShipping(shippingId, false);
                if (shipping == null)
                    throw new NotFoundException(nameof(shipping), shippingId);

                _repository.Remove(shipping);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(shipping));
                }
                return new ServiceResult();
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }           
        }     
    }
}
