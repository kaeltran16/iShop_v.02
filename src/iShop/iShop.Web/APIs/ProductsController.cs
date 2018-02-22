using System.Linq;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Common.Helpers;
using iShop.Service.Interfaces;
using iShop.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    [Route("/api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //// POST
        ////[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SavedProductDto savedProductDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApplicationError()
                {
                    Error = ModelState.GetError()
                });
                  
            var result = await _productService.CreateAsync(savedProductDto);
            return result.IsSuccess
                ? CreatedAtRoute(ApplicationConstants.ControllerName.Product, new {id = result.Payload.Id}, result)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());
        }

        // GET
        [HttpGet("{id}", Name = ApplicationConstants.ControllerName.Product)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _productService.GetSingleAsync(id);

            return result.IsSuccess
                ? Ok(result.Payload)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());
        }

        // GET 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();

            return result.IsSuccess
                ? Ok(result.Payload)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());
        }

      

        //// PUT
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SavedProductDto savedProductResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApplicationError()
                {
                    Error = ModelState.GetError()
                });

            var result = await _productService.UpdateAsync(id, savedProductResource);
            return result.IsSuccess
                ? Ok(result.Payload)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());        }

        //// DELETE
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productService.RemoveAsync(id);
            return result.IsSuccess
                ? Ok(id)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());       
        }
    }
}