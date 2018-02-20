using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Data.Entities;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Interfaces;
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

        public async Task<ShippingDto> CreateAsync(ShippingDto shippingDto)
        {
            var shipping = _mapper.Map<ShippingDto, Shipping>(shippingDto);
            await _repository.AddAsync(shipping);
            await _unitOfWork.CompleteAsync();
            return await Get(shipping.Id);
        }

        public async Task<ShippingDto> Get(Guid id)
        {
            var shipping = await _repository.GetShipping(id);
            return _mapper.Map<Shipping, ShippingDto>(shipping);
        }

        public async Task<IEnumerable<ShippingDto>> GetAll()
        {
            var shippings = await _repository.GetShippings();
            return _mapper.Map<IEnumerable<Shipping>, IEnumerable<ShippingDto>>(shippings);
        }

        public async Task RemoveAsync(Guid shippingId)
        {
            var shipping = await _repository.GetShipping(shippingId, false);
            _repository.Remove(shipping);
            await _unitOfWork.CompleteAsync();
        }


        public async Task<ShippingDto> UpdateAsync(Guid shippingId, ShippingDto shippingDto)
        {
            var shipping = await _repository.GetShipping(shippingId);
            _mapper.Map(shippingDto, shipping);

            await _unitOfWork.CompleteAsync();

            return await Get(shipping.Id);
        }
    }
}
