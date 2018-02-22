using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using Microsoft.AspNetCore.Http;

namespace iShop.Service.Interfaces
{
    public interface IImageService
    {
        Task<ImageDto> Upload(Guid productId, IFormFile file);
        Task Remove(Guid imageId);
    }
}
