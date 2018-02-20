using System;
using System.Collections.Generic;
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
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _repository;

        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetRepository<ISupplierRepository>();
        }
        public async Task<SupplierDto> Get(Guid id)
        {
            var supplier = await _repository.GetSupplier(id);
            return _mapper.Map<Supplier, SupplierDto>(supplier);
        }

        public async Task<IEnumerable<SupplierDto>> GetAll()
        {
            var suppliers = await _repository.GetSuppliers();
            return _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDto>>(suppliers);
        }

        public async Task<SupplierDto> CreateAsync(SupplierDto supplierDto) 
        {
            var supplier = _mapper.Map<SupplierDto, Supplier>(supplierDto);
            await _repository.AddAsync(supplier);
            await _unitOfWork.CompleteAsync();
            return await Get(supplier.Id);
        }

        public async Task<SupplierDto> UpdateAsync(Guid id, SupplierDto supplierDto)
        {
            var supplier = await _repository.GetSupplier(id);
            _mapper.Map(supplierDto, supplier);
            _repository.Update(supplier);
            await _unitOfWork.CompleteAsync();
            return await Get(supplier.Id);
        }

        public async Task RemoveAsync(Guid id)
        {
            var supplier = await _repository.GetSupplier(id);
            _repository.Remove(supplier);
            await _unitOfWork.CompleteAsync();
        }
    }
}
