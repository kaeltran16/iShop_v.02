using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.DTOs;
using iShop.Common.Helpers;
using iShop.Data.Entities;
using iShop.Repo.Data.Implementations;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Interfaces;
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

        public async Task<ImageDto> Upload(Guid productId, IFormFile file)
        {
            var product = await _unitOfWork.GetRepository<ProductRepository>().GetProduct(productId);
            var uploadFolderPath = Path.Combine(_host.WebRootPath, "images");

            // Create a folder if the folder does not exist
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var image = new Image { FileName = fileName, ProductId = productId };
            await _repository.AddAsync(image);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Image, ImageDto>(image);
        }

        public async Task Remove(Guid imageId)
        {
            var image = await _repository.Get(imageId, true);
            _repository.Remove(image);
            await _unitOfWork.CompleteAsync();
        }
        
    }
}
