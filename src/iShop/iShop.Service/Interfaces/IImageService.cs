using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.DTOs;
using iShop.Service.Base;
using iShop.Service.Commons;
using Microsoft.AspNetCore.Http;

namespace iShop.Service.Interfaces
{
    public interface IImageService: IServiceBase
    {
        Task<IServiceResult> UploadAsync(string id, IFormFile file);
        Task<IServiceResult> RemoveAsync(string id);
    }
}
