using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Common.Exceptions;
using iShop.Common.Extensions;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Repo.Data.Implementations;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Interfaces;
using iShop.Service.Commons;
using iShop.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace iShop.Service.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _host;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ImageSettings _imageSettings;
        private readonly IImagesRepository _repository;

        public ImageService(IHostingEnvironment host,IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<ImageSettings> imageSettings)
        {
            _host = host;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageSettings = imageSettings.Value;
            _repository = _unitOfWork.GetRepository<IImagesRepository>();
        }

        public async Task<IServiceResult> UploadAsync(string id, IFormFile file)
        {
            try
            {   var productId = id.ToGuid();

                var product = await _unitOfWork.GetRepository<ProductRepository>().GetSingleAsync(productId);
                if (product == null)
                    throw new NotFoundException(nameof(product), productId);

                var imageValidationResult = ValidateImageFile(file);

                if (!imageValidationResult.IsSuccess)
                    return imageValidationResult;

                var fileName = GenerateRandomFileName(file);

                var filePath = GenerateFolderPath(fileName);

                await CopyFileToRootAsync(file, filePath);

                var image = new Image { FileName = fileName, ProductId = productId };
                await _repository.AddAsync(image);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(product));
                };
                var imageDto = _mapper.Map<Image, ImageDto>(image);
                return new ServiceResult(payload: imageDto);
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }
         
        }

        public async Task<IServiceResult> RemoveAsync(string id)
        {
            try
            {
                var imageId = id.ToGuid();

                var image = await _repository.GetSingleAsync(imageId, true);
                if (image == null)
                    throw new NotFoundException(nameof(image), imageId);

                _repository.Remove(image);
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new SaveFailedException(nameof(image));
                };
                return new ServiceResult();
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }          
        }

        private IServiceResult ValidateImageFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    throw new NullOrEmptyException(nameof(file), file);

                if (file.Length > _imageSettings.MaxByte)
                    throw new OversizeException(nameof(file), _imageSettings.MaxByte);

                if (!_imageSettings.IsSupported(file.FileName))
                    throw new NotSupportException(nameof(file), Path.GetExtension(file.FileName));

                return new ServiceResult();
            }
            catch (Exception e)
            {
                return new ServiceResult(false, e.Message);
            }
        }

        private string GenerateRandomFileName(IFormFile file)
        {
            try
            {
                return Guid.NewGuid().ToString("N")
                       + DateTime.Now.ToString("ddMMyyyy")
                       + Path.GetExtension(file.FileName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }          
        }

        private string GenerateFolderPath(string fileName)
        {
            try
            {
                var uploadFolderPath = Path.Combine(_host.WebRootPath, "images");

                // Create a folder if the folder does not exist
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                var filePath = Path.Combine(uploadFolderPath, fileName);
                return filePath;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }           
        }

        private async Task CopyFileToRootAsync(IFormFile file, string filePath)
        {
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception e)
            {
               throw new Exception(e.Message);
            }

        } 
    }
}
