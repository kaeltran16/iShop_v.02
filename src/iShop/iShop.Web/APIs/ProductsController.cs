using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Repo.Data.Implementations;
using iShop.Repo.UnitOfWork.Interfaces;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET
        [HttpGet("{id}", Name = ApplicationConstants.ControllerName.Product)]
        public async Task<IActionResult> Get(string id)
        {
            bool isValid = Guid.TryParse(id, out var productId);
            //if (!isValid)
            //    return InvalidId(id);

            var productDto = await _productService.Get(productId);

            //if (product == null)
            //    NotFound(productId);

            //var productResource = _mapper.Map<Product, ProductDto>(product);

            return Ok(productDto);
        }

        // GET 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
                
            return Ok(products);
        }

        // POST
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SavedProductDto savedProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = await _productService.CreateAsync(savedProductDto);
            return CreatedAtRoute(ApplicationConstants.ControllerName.Product, new { id = product.Id }, product);
        }

        //// PUT
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SavedProductDto savedProductResource)
        {
            bool isValid = Guid.TryParse(id, out var productId);
            //if (!isValid)
            //    return InvalidId(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.UpdateAsync(productId, savedProductResource);
            return Ok(product);

            //var product = await _productService.Get(productId);

            //if (product == null)
            //    return NotFound(productId);

            //_mapper.Map(savedProductResource, product);

            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.SavedFail,  ApplicationConstants.ControllerName.Product, product.Id);
            //    _logger.LogInformation("");
            //    return FailedToSave(product.Id);
            //}

            //product = await _unitOfWork.ProductRepository.GetProduct(product.Id);

            //var result = _mapper.Map<Product, SavedProductDto>(product);

            ////_logger.LogMessage(LoggingEvents.Updated,  ApplicationConstants.ControllerName.Product, product.Id);
            //_logger.LogInformation("");
            //return Ok(result);
        }

        //// DELETE
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var productId);
            //if (!isValid)
            //    return InvalidId(id);

            //var product = await _unitOfWork.ProductRepository.GetProduct(productId);

            //if (product == null)
            //    return NotFound(productId);

            //_unitOfWork.ProductRepository.Remove(product);
            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.SavedFail,  ApplicationConstants.ControllerName.Product, product.Id);
            //    _logger.LogInformation("");
            //    return FailedToSave(product.Id);
            //}

            ////_logger.LogMessage(LoggingEvents.Deleted,  ApplicationConstants.ControllerName.Product, product.Id);
            //_logger.LogInformation("");

            //return NoContent();

            await _productService.RemoveAsync(productId);
            return NoContent();
        }
    }
}