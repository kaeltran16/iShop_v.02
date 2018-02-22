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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetRepository<ICategoryRepository>();
        }
        public async Task<CategoryDto> Get(Guid id)
        {
            var category = await _repository.GetCategory(id);
            return _mapper.Map<Category, CategoryDto>(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await _repository.GetCategories();
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto categoryDto) 
        {
            var category = _mapper.Map<CategoryDto, Category>(categoryDto);
            await _repository.AddAsync(category);
            await _unitOfWork.CompleteAsync();
            return await Get(category.Id);
        }

        public async Task<CategoryDto> UpdateAsync(Guid id, CategoryDto categoryDto)
        {
            var category = await _repository.GetCategory(id);
            _mapper.Map(categoryDto, category);
            _repository.Update(category);
            await _unitOfWork.CompleteAsync();
            return await Get(category.Id);
        }

        public async Task RemoveAsync(Guid id)
        {
            var category = await _repository.GetCategory(id);
            _repository.Remove(category);
            await _unitOfWork.CompleteAsync();
        }
    }
}
