using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// This class is responsible for providing services for the Product
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                    }
                }

                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(product));
                }

                var result = await GetSingleAsync(product.Id.ToString());
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
                var productId = id.ToGuid();
                var product = await _repository.GetProduct(productId);

                if (product == null)
                    throw new NotFoundException(nameof(product), id);

                var productDto = _mapper.Map<Product, ProductDto>(product);
                return new ServiceResult(payload: productDto);
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
                var products = await _repository.GetProducts();
                var productsDto = _mapper.Map<IEnumerable<Product>,
                    IEnumerable<ProductDto>>(products);

                return new ServiceResult(payload: productsDto);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }           
        }

        public async Task<IServiceResult> UpdateAsync(string id, SavedProductDto productDto)
        {
            try
            {
                var productId = id.ToGuid();
                var product = await _repository.GetProduct(productId);
                _mapper.Map(productDto, product);
              
                AddOrRemoveCategories(product, productDto);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(product));
                }

                var result = await GetSingleAsync(product.Id.ToString());
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
                var productId = id.ToGuid();
                var product = await _repository.GetProduct(productId, false);
                if (product == null)
                    throw new NotFoundException(nameof(product), productId);

                _repository.Remove(product);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(product));
                }
                return new ServiceResult();
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }
          
        }

        private void AddOrRemoveCategories(Product product, SavedProductDto productDto)
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
            var product = await _repository.GetProduct(productId);
            if (product == null)
                throw new NotFoundException(nameof(product), productId);
            product.RemoveCategory(categoryId);
        }

    }
}
