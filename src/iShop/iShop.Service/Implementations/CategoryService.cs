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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        private readonly ICategoryRepository _repository;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _repository = _unitOfWork.GetRepository<ICategoryRepository>();
        }

        public async Task<IServiceResult> CreateAsync(CategoryDto categoryDto)
        {
            try
            {
                var category = _mapper.Map<CategoryDto, Category>(categoryDto);
                await _repository.AddAsync(category);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(category));
                }
                _logger.LogInformation($"Added new {nameof(category)} with id: {category.Id}.");
                var result = await GetSingleAsync(category.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError($"Adding new category failed. {e.Message}");
                return new ServiceResult(false, e.Message);
            }

        }

        public async Task<IServiceResult> GetSingleAsync(string id)
        {
            try
            {
                var categoryId = id.ToGuid();
                var category = await _repository.GetSingleAsync(categoryId);
                if (category == null)
                    throw new NotFoundException(nameof(category), categoryId);

                var categoryDto = _mapper.Map<Category, CategoryDto>(category);

                return new ServiceResult(payload: categoryDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting a category with id: {id} failed. {e.Message}");
                return new ServiceResult(false, e.Message);
            }
        }


        public async Task<IServiceResult> GetAllAsync(QueryObject queryTerm = null)
        {
            try
            {
                var categories = queryTerm != null
                    ? _repository.SortAndFilterAsync(queryTerm).Result.Items
                    : await _repository.GetAllAsync();

                var categoriesDto = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);

                return new ServiceResult(payload: categoriesDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting all categories failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> UpdateAsync(string id, CategoryDto categoryDto)
        {
            try
            {
                var categoryId = id.ToGuid();

                var category = await _repository.GetSingleAsync(categoryId);

                _mapper.Map(categoryDto, category);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(category));
                }
                _logger.LogInformation($"Updated {nameof(category)} with id: {category.Id}.");
                var result = await GetSingleAsync(category.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError($"Updating category with id: {id} failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> RemoveAsync(string id)
        {
            try
            {
                var categoryId = id.ToGuid();
                var category = await _repository.GetSingleAsync(categoryId);
                if (category == null)
                    throw new NotFoundException(nameof(category), categoryId);

                _repository.Remove(category);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(category));
                }
                _logger.LogInformation($"Delete {nameof(category)} with id: {category.Id}.");

                return new ServiceResult();

            }
            catch (Exception e)
            {
                _logger.LogError($"Deleting category with id: {id} failed.", e.Message);

                return new ServiceResult(false, e.Message);
            }

        }
    }
}
