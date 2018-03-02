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
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SupplierService> _logger;
        private readonly ISupplierRepository _repository;

        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SupplierService> logger)
        { 
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _repository = _unitOfWork.GetRepository<ISupplierRepository>();
        }

        public async Task<IServiceResult> CreateAsync(SupplierDto supplierDto)
        {
            try
            {
                var supplier = _mapper.Map<SupplierDto, Supplier>(supplierDto);

                await _repository.AddAsync(supplier);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(supplier));
                }
                _logger.LogInformation($"Added new {nameof(supplier)} with id: {supplier.Id}");

                var result = await GetSingleAsync(supplier.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError($"Adding new supplier failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> GetSingleAsync(string id)
        {
            try
            {
                var supplierId = id.ToGuid();

                var supplier = await _repository.GetSingleAsync(supplierId);
                if (supplier == null)
                    throw new NotFoundException(nameof(supplier), supplierId);

                var supplierDto = _mapper.Map<Supplier, SupplierDto>(supplier);

                return new ServiceResult(payload: supplierDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting a supplier with id: {id} failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }

        }

        public async Task<IServiceResult> GetAllAsync(QueryObject queryTerm)
        {
            try
            {
                var suppliers = queryTerm != null 
                    ? _repository.SortAndFilterAsync(queryTerm).Result.Items 
                    : await _repository.GetAllAsync();
                var suppliersDto = _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDto>>(suppliers);

                return new ServiceResult(payload: suppliersDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting all suppliers failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }
        public async Task<IServiceResult> UpdateAsync(string id, SupplierDto supplierDto)
        {
            try
            {
                var supplierId = id.ToGuid();

                var supplier = await _repository.GetSingleAsync(supplierId);

                _mapper.Map(supplierDto, supplier);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(supplier));
                }
                _logger.LogInformation($"Updated {nameof(supplier)} with id: {supplier.Id}");

                var result = await GetSingleAsync(supplier.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError($"Updating supplier with id: {id} failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }

        }

        public async Task<IServiceResult> RemoveAsync(string id)
        {
            try
            {
                var supplierId = id.ToGuid();

                var supplier = await _repository.GetSingleAsync(supplierId);
                if (supplier == null)
                    throw new NotFoundException(nameof(supplier), supplierId);

                _repository.Remove(supplier);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(supplier));
                }
                _logger.LogInformation($"Delete {nameof(supplier)} with id: {supplier.Id}");

                return new ServiceResult();
            }
            catch (Exception e)
            {
                _logger.LogError($"Deleting supplier with id: {id} failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }
    }
}
