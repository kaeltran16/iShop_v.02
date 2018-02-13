using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Data.Entities;
using iShop.Repo.Data.Implementations;
using iShop.Repo.UnitOfWork.Interfaces;
using iShop.Service.Interfaces;

namespace iShop.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            var product = await _unitOfWork.GetRepository<ProductRepository>().GetProduct(productId);
            product.RemoveCategory(categoryId);
        }

        public async Task<ProductDto> CreateAsync(SavedProductDto productDto)
        {
            if (productDto == null)
                throw new ArgumentException();
            var product = _mapper.Map<SavedProductDto, Product>(productDto);

            await _unitOfWork.GetRepository<ProductRepository>().AddAsync(product);

            AddToInventory(product, productDto.SupplierId, productDto.Stock);
            if (productDto.Categories.Count > 0)
            {
                foreach (var c in productDto.Categories)
                {
                    AddCategory(product, c);
                }
            }

            await _unitOfWork.CompleteAsync();
            product = await _unitOfWork.GetRepository<ProductRepository>().GetProduct(product.Id);
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<ProductDto> Get(Guid id)
        {
            var product = await _unitOfWork.GetRepository<ProductRepository>().GetProduct(id);
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var products = await _unitOfWork.GetRepository<ProductRepository>().GetProducts();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }

        public async Task RemoveAsync(Guid productId)
        {
            var product = await _unitOfWork.GetRepository<ProductRepository>().GetProduct(productId, false);
            _unitOfWork.GetRepository<ProductRepository>().Remove(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<ProductDto> UpdateAsync(Guid productId, SavedProductDto productDto)
        {
            var product = await _unitOfWork.GetRepository<ProductRepository>().GetProduct(productId);
            _mapper.Map(productDto, product);
            product.Inventory.SupplierId = productDto.SupplierId;
            product.Inventory.Stock = productDto.Stock;
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

            await _unitOfWork.CompleteAsync();

            var result = await _unitOfWork.GetRepository<ProductRepository>().GetProduct(product.Id);
            return _mapper.Map<Product, ProductDto>(result);
        }
    }
}
