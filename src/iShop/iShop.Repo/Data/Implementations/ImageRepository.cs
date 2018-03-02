using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Repo.Data.Implementations
{
    public class ImageRepository : DataRepositoryBase<Image>, IImagesRepository
    {
        public ImageRepository(ApplicationDbContext context)
            : base(context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Image>> GetProductImages(Guid productId, bool isIncludeRelavtive)
        {
            var spec =
                isIncludeRelavtive
                    ? new Specification<Image>(predicate: i => i.ProductId == productId,
                        includes: null)
                    : new Specification<Image>(predicate: i => i.ProductId == productId,
                        includes: source => source.Include(i => i.Product));

            return await Get(spec).ToListAsync();
        }

        public override Func<IQueryable<Image>, IIncludableQueryable<Image, object>> CreateInclusiveRelatives()
        {
            return
                source => source.Include(i => i.Product);
        }

        public override Dictionary<string, Expression<Func<Image, object>>> CreateQueryTerms()
        {
            return null;
        }
    }
}
