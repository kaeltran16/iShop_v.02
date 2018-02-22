using System;
using System.Threading.Tasks;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.APIs
{
    /// <summary>
    /// This controller handles create request for creating a image associated with a given product  
    /// </summary>
    [Route("/api/product/{productId}/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }
        //[Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPost]
        public async Task<IActionResult> UpLoad(string productId, IFormFile file)
        {
            bool isValid = Guid.TryParse(productId, out var id);

            //if (!isValid)
            //    return InvalidId(productId);

            //var product = await _unitOfWork.ProductRepository.GetProduct(id, false);

            //if (product == null)
            //    return NotFound(id);

            //if (file == null || file.Length == 0)
            //    return NullOrEmpty();

            //if (file.Length > _imageSettings.MaxByte)
            //    return InvalidSize(ApplicationConstants.ControllerName.Image, _imageSettings.MaxByte);

            //if (!_imageSettings.IsSupported(file.FileName))
            //    return UnSupportedType(_imageSettings.AcceptedTypes);


            //var uploadFolderPath = Path.Combine(_host.WebRootPath, "images");

            //// Create a folder if the folder does not exist
            //if (!Directory.Exists(uploadFolderPath))
            //{
            //    Directory.CreateDirectory(uploadFolderPath);
            //}

            //var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            //var filePath = Path.Combine(uploadFolderPath, fileName);

            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}

            //var image = new Image { FileName = fileName, ProductId = id };
            //await _unitOfWork.ImageRepository.AddAsync(image);

            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.Fail,  ApplicationConstants.ControllerName.Image, image.Id);
            //    _logger.LogInformation("");
            //    return FailedToSave(image.Id);
            //}
            ////_logger.LogMessage(LoggingEvents.Created,  ApplicationConstants.ControllerName.Image, image.Id);
            //_logger.LogInformation("");
            //return Ok(_mapper.Map<Image, ImageDto>(image));
            //}

            var imageDto = await _imageService.Upload(id, file);
            return Ok(imageDto);
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
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var imageId);

            //if (!isValid)
            //    return InvalidId(id);

            //var image = await _unitOfWork.ImageRepository.Get(imageId);

            //if (image == null)
            //    return NullOrEmpty();

            //_unitOfWork.ImageRepository.Remove(image);
            //if (!await _unitOfWork.CompleteAsync())
            //{
            //    //_logger.LogMessage(LoggingEvents.Fail, ApplicationConstants.ControllerName.Category, image.Id);
            //    _logger.LogInformation("");
            //    return FailedToSave(image.Id);
            //}

            ////_logger.LogMessage(LoggingEvents.Deleted, ApplicationConstants.ControllerName.Category, image.Id);
            //_logger.LogInformation("");
            //return NoContent();
            await _imageService.Remove(imageId);
            return NoContent();
        }
    }

}
