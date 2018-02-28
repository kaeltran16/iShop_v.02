using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface IImagesRepository: IDataRepository<Image>
    {
        Task<IEnumerable<Image>> GetProductImages(Guid productId, bool isIncludeRelavtive);
    }
}
