using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IImagesRepository: IDataRepository<ImageEntity>
    {
        Task<IEnumerable<ImageEntity>> GetProductImages(Guid productId, bool isIncludeRelavtive);
        Task<ImageEntity> Get(Guid id, bool isIncludeRelavtive);
    }
}
