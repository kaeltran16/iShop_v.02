using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;

namespace iShop.Repo.Data.Implementations
{
    public class ImageRepository: DataRepositoryBase<Image>, IImagesRepository
    {
        public ImageRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Image>> GetProductImages(Guid productId)
        {
            return await GetAllAsync(i => i.ProductId == productId);
        }

        public async Task<Image> Get(Guid id)
        {
            return await GetSingleAsync(i => i.Id == id);
        }
    }
}
