//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using AutoMapper;
//using iShop.Common.DTOs;
//using iShop.Common.Exceptions;
//using iShop.Common.Extensions;
//using iShop.Data.Entities;
//using iShop.Repo.Data.Interfaces;
//using iShop.Repo.UnitOfWork.Interfaces;
//using iShop.Service.Commons;
//using iShop.Service.Interfaces;

//namespace iShop.Service.Implementations
//{
//    public class CategoryService : ICategoryService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;
//        private readonly ICategoryRepository _repository;

//        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//            _repository = _unitOfWork.GetRepository<ICategoryRepository>();
//        }

//        public async Task<IServiceResult> CreateAsync(CategoryDto categoryDto) 
//        {
//            try
//            {  
//                var category = _mapper.Map<CategoryDto, Category>(categoryDto);
//                await _repository.AddAsync(category);
//                if (!await _unitOfWork.CompleteAsync())
//                {
//                    throw new SaveFailedException(nameof(category));
//                }               
//                var result = await GetSingleAsync(category.Id.ToString());
//                return new ServiceResult(payload: result.Payload);
//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }
          
//        }

//        public async Task<IServiceResult> GetSingleAsync(string id)
//        {
//            try
//            {
//                var categoryId = id.ToGuid();
//                var category = await _repository.GetCategory(categoryId);
//                if (category == null)
//                    throw new NotFoundException(nameof(category), categoryId);

//                var categoryDto = _mapper.Map<Category, CategoryDto>(category);

//                return new ServiceResult(payload: categoryDto);
//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }          
//        }

//        public async Task<IServiceResult> GetAllAsync()
//        {
//            try
//            {
//                var categories = await _repository.GetCategories();
//                var categoriesDto = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);

//                return new ServiceResult(payload: categoriesDto);
//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }          
//        }

//        public async Task<IServiceResult> UpdateAsync(string id, CategoryDto categoryDto)
//        {
//            try
//            {
//                var categoryId = id.ToGuid();
            
//                var category = await _repository.GetCategory(categoryId);

//                _mapper.Map(categoryDto, category);
//                if (!await _unitOfWork.CompleteAsync())
//                {
//                    throw new SaveFailedException(nameof(category));
//                }              
//                var result = await GetSingleAsync(category.Id.ToString());
//                return new ServiceResult(payload: result.Payload);
//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }       
//        }

//        public async Task<IServiceResult> RemoveAsync(string id)
//        {
//            try
//            {
//                var categoryId = id.ToGuid();
//                var category = await _repository.GetCategory(categoryId);
//                if (category == null)
//                    throw new NotFoundException(nameof(category), categoryId);

//                _repository.Remove(category);
//                if (!await _unitOfWork.CompleteAsync())
//                {
//                    throw new SaveFailedException(nameof(category));
//                }
//                return new ServiceResult();

//            }
//            catch (Exception e)
//            {
//                return new ServiceResult(false, e.Message);
//            }
           
//        }
//    }
//}
