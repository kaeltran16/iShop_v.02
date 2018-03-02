using System.Threading.Tasks;
using iShop.Common.Helpers;
using iShop.Service.Interfaces;
using iShop.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.APIs
{
    /// <summary>
    /// This controller handles create request for creating a image associated with a given product  
    /// </summary>
    [Route("/api/product/{productId}/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IImageService imageService, ILogger<ImagesController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPost]
        public async Task<IActionResult> UploadAsync(string productId, IFormFile file)
        {
            var result = await _imageService.UploadAsync(productId, file);
            if (result.IsSuccess)
                return Ok(result.Payload);

            _logger.LogError(
                $"Uploading image with id: {result.Payload.Id} failed. {result.Message}");
            return StatusCode(500, new ApplicationError() { Error = result.Message }.ToString());
        }

        //[HttpGet("{productId}")]
        //public async Task<IActionResult> Get(Guid productId)
        //{
        //    var images = await _unitOfWork.ImageRepository.GetProductImages(productId);
        //    var imageResource = _mapper.Map<IEnumerable<Image>, IEnumerable<ImageResource>>(images);
        //    return Ok(imageResource);
        //}

        // DELETE
        //[Authorize(Policy = "SuperUsers")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _imageService.RemoveAsync(id);
            if (result.IsSuccess)
                return Ok(result.Payload);

            _logger.LogError(
                $"Deleting image with id: {result.Payload.Id} failed. {result.Message}");
            return StatusCode(500, new ApplicationError() { Error = result.Message }.ToString());
        }
    }

}
