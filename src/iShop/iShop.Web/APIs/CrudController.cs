using System.Threading.Tasks;
using iShop.Common.Base;
using iShop.Common.Helpers;
using iShop.Service.Base;
using iShop.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    public abstract class CrudController<TService, TDto> : Controller
    where TService: ICrudServiceBase<TDto>
    where TDto: class, ISavedBaseDto
    {
        private readonly TService _service;


        protected CrudController(TService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApplicationError()
                {
                    Error = ModelState.GetError()
                });
                  
            var result = await _service.CreateAsync(dto);
            return result.IsSuccess
                ? Ok(result.Payload)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(string id)
        {
            var result = await _service.GetSingleAsync(id);

            return result.IsSuccess
                ? Ok(result.Payload)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();

            return result.IsSuccess
                ? Ok(result.Payload)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] TDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApplicationError()
                {
                    Error = ModelState.GetError()
                });

            var result = await _service.UpdateAsync(id, dto);
            return result.IsSuccess
                ? Ok(result.Payload)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());     
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _service.RemoveAsync(id);
            return result.IsSuccess
                ? Ok(id)
                : StatusCode(500, new ApplicationError() {Error = result.Message}.ToString());     
        }


    }
}
