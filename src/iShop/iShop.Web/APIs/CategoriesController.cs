using System;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Common.Helpers;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    /// <summary>
    /// This controller handles crud request for Cactegory
    /// </summary>
    [Route("/api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAll();

            return Ok(categories);
        }


        // GET
        [HttpGet("{id}", Name = ApplicationConstants.ControllerName.Category)]
        public async Task<IActionResult> Get(string id)
        {
            // Validate the input id, make sure it has the correct format
            bool isValid = Guid.TryParse(id, out var categoryId);

            //if (!isValid)
            //    return InvalidId(id);

            //var category = await _unitOfWork.CategoryRepository.GetCategory(categoryId);

            //if (category == null)
            //    return NullOrEmpty();

            //var categoryResource = _mapper.Map<Category, CategoryDto>(category);

            //return Ok(categoryResource);
            var category = await _categoryService.Get(categoryId);
            return Ok(category);
        }



        // POST
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto categoryResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var category = _mapper.Map<CategoryDto, Category>(categoryResources);

            //await _unitOfWork.CategoryRepository.AddAsync(category);

            //// if something happens and the new item can not be saved, return the error
            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    _logger.LogInformation("");
            //    return FailedToSave(category.Id);
            //}

            //category = await _unitOfWork.CategoryRepository.GetCategory(category.Id);

            //var result = _mapper.Map<Category, CategoryDto>(category);
            //_logger.LogInformation("");

            var category = await _categoryService.CreateAsync(categoryResources);
            //_logger.LogMessage(LoggingEvents.Created, ApplicationConstants.ControllerName.Category, category.Id);
            return CreatedAtRoute(ApplicationConstants.ControllerName.Category, new { id = category.Id }, category);
        }

        // DELETE
        //[Authorize(Roles = ApplicationConstants.RoleName.SuperUser)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var categoryId);

            //if (!isValid)
            //    return InvalidId(id);

            //var category = await _unitOfWork.CategoryRepository.GetCategory(categoryId);

            //if (category == null)
            //    return NullOrEmpty();

            //_unitOfWork.CategoryRepository.Remove(category);
            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    _logger.LogInformation("");
            //    //_logger.LogMessage(LoggingEvents.Fail, ApplicationConstants.ControllerName.Category, category.Id);
            //    return FailedToSave(category.Id);
            //}

            ////_logger.LogMessage(LoggingEvents.Deleted, ApplicationConstants.ControllerName.Category, category.Id);
            //_logger.LogInformation("");
            await _categoryService.RemoveAsync(categoryId);
            return NoContent();
        }



        // PUT
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryDto categoryResource)
        {
            bool isValid = Guid.TryParse(id, out var categoryId);

            //if (!isValid)
            //    return InvalidId(id);

            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            //var category = await _unitOfWork.CategoryRepository.GetCategory(categoryId);

            //if (category == null)
            //    return NullOrEmpty();

            //_mapper.Map(categoryResource, category);

            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.SavedFail, ApplicationConstants.ControllerName.Category, category.Id);
            //    _logger.LogInformation("");
            //    return FailedToSave(category.Id);
            //}

            //category = await _unitOfWork.CategoryRepository.GetCategory(category.Id);

            //var result = _mapper.Map<Category, CategoryDto>(category);
            ////_logger.LogMessage(LoggingEvents.Updated, ApplicationConstants.ControllerName.Category, category.Id);
            //_logger.LogInformation("");
            var category = await _categoryService.UpdateAsync(categoryId, categoryResource);
            return Ok(category);
        }

    }
}