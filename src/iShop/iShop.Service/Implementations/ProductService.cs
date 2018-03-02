using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// This class is responsible for providing services for the Product
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _repository;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _repository = _unitOfWork.GetRepository<IProductRepository>();
        }

        public async Task<IServiceResult> CreateAsync(SavedProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<SavedProductDto, Product>(productDto);

                await _repository.AddAsync(product);

                AddToInventory(product, productDto.SupplierId, productDto.Stock);

                if (productDto.Categories.Count > 0)
                {
                    foreach (var c in productDto.Categories)
                    {
                        AddCategory(product, c);
                        _logger.LogError($"Added product with id {product.Id}.");
                    }
                }

                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(product));
                }
                _logger.LogInformation($"Added new {nameof(product)} with id: {product.Id}");

                var result = await GetSingleAsync(product.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError($"Adding new product failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }

        }

        public async Task<IServiceResult> GetSingleAsync(string id)
        {
            try
            {
                var productId = id.ToGuid();
                var product = await _repository.GetSingleAsync(productId);

                if (product == null)
                    throw new NotFoundException(nameof(product), id);

                var productDto = _mapper.Map<Product, ProductDto>(product);
                return new ServiceResult(payload: productDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting a product with id: {id} failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> GetAllAsync(QueryObject queryTerm = null)
        {
            try
            {
                var products = queryTerm != null 
                    ? _repository.SortAndFilterAsync(queryTerm).Result.Items 
                    : await _repository.GetAllAsync();
           
                var productsDto = _mapper.Map<IEnumerable<Product>,
                    IEnumerable<ProductDto>>(products);

                return new ServiceResult(payload: productsDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Getting all products failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }
        }

        public async Task<IServiceResult> UpdateAsync(string id, SavedProductDto productDto)
        {
            try
            {
                var productId = id.ToGuid();
                var product = await _repository.GetSingleAsync(productId);
                _mapper.Map(productDto, product);

                AddOrRemoveCategories(product, productDto);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(product));
                }
                _logger.LogInformation($"Updated {nameof(product)} with id: {product.Id}");

                var result = await GetSingleAsync(product.Id.ToString());
                return new ServiceResult(payload: result.Payload);
            }
            catch (Exception e)
            {
                _logger.LogError($"Updating product with id: {id} failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }

        }

        public async Task<IServiceResult> RemoveAsync(string id)
        {
            try
            {
                var productId = id.ToGuid();
                var product = await _repository.GetSingleAsync(productId, false);
                if (product == null)
                    throw new NotFoundException(nameof(product), productId);

                _repository.Remove(product);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(product));
                }
                _logger.LogInformation($"Delete {nameof(product)} with id: {product.Id}");

                return new ServiceResult();
            }
            catch (Exception e)
            {
                _logger.LogError($"Deleting product with id: {id} failed. {e.Message}");

                return new ServiceResult(false, e.Message);
            }

        }

        private void AddOrRemoveCategories(Product product, SavedProductDto productDto)
        {
            try
            {
                var addedCategories =
                    productDto.Categories.Where(id => product.ProductCategories.All(pd => pd.CategoryId != id)).ToList();
                if (addedCategories.Any())
                {
                    foreach (var category in addedCategories)
                    {
                        product.AddCategory(category);
                    }
                }
                var removedCategories =
                    product.ProductCategories.Where(c => !productDto.Categories.Contains(c.CategoryId)).ToList();
                if (removedCategories.Any())
                {
                    foreach (var category in addedCategories)
                    {
                        product.RemoveCategory(category);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AddToInventory(Product product, Guid supplierId, int stock)
        {
            product.AddToInventory(stock, supplierId);
        }

        public void AddCategory(Product product, Guid categoryId)
        {
            product.AddCategory(categoryId);
        }

        public async Task RemoveCategory(Guid productId, Guid categoryId)
        {
            var product = await _repository.GetSingleAsync(productId);
            if (product == null)
                throw new NotFoundException(nameof(product), productId);
            product.RemoveCategory(categoryId);
        }

    }
}
