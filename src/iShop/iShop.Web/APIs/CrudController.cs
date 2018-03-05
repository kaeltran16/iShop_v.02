using System.Threading.Tasks;
using iShop.Common.Helpers;
using iShop.Data.Base;
using iShop.Service.Base;
using iShop.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    public abstract class CrudController<TEntity, TDto, TService> : Controller
    where TEntity : IEntityBase
    where TDto : ISavedBaseDto
    where TService : ICrudServiceBase<TDto>
    {
        private readonly ICrudServiceBase<TDto> _service;
        private readonly ILogger<TService> _logger;


        protected CrudController(TService service, ILogger<TService> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError(
                    $"Request creating new {typeof(TEntity).Name} failed. {ModelState.GetError()}");
                return BadRequest(new ApplicationError()
                {
                    Error = ModelState.GetError()
                });
            }

            var result = await _service.CreateAsync(dto);
            if (result.IsSuccess)
                return Ok(result.Payload);

            _logger.LogError($"Request creating new {typeof(TEntity).Name} failed. {result.Message}");
            return StatusCode(500, new ApplicationError() { Error = result.Message }.ToString());

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleAsync(string id)
        {
            var result = await _service.GetSingleAsync(id);

            if (result.IsSuccess)
                return Ok(result.Payload);

            _logger.LogError(
                $"Request getting {typeof(TEntity).Name} with id: {id} failed. {result.Message}");
            return StatusCode(500, new ApplicationError() { Error = result.Message }.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(QueryObject queryTerm)
        {
            var result = await _service.GetAllAsync(queryTerm);

            if (result.IsSuccess)
                return Ok(result.Payload);

            _logger.LogError(
                $"Request getting all {typeof(TEntity).Name}s failed. {result.Message}");
            return StatusCode(500, new ApplicationError() { Error = result.Message }.ToString());
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
            if (result.IsSuccess)
                return Ok(result.Payload);

            _logger.LogError(
                $"Request updating {typeof(TEntity).Name} with id: {id} failed. {result.Message}");
            return StatusCode(500, new ApplicationError() { Error = result.Message }.ToString());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _service.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result.Payload);

            _logger.LogError(
                $"Request deleting {typeof(TEntity).Name} with id: {id} failed. {result.Message}");
            return StatusCode(500, new ApplicationError() { Error = result.Message }.ToString());
        }


    }
}
