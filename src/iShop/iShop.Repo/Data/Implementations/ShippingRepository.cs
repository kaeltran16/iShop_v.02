using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Data.Models;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iShop.Repo.Data.Implementations
{
    public class ShippingRepository: DataRepositoryBase<ShippingEntity>, IShippingRepository
    {
        public ShippingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ShippingEntity> GetShipping(Guid id, bool isIncludeRelative = true)
        {
            ISpecification<ShippingEntity> spec = isIncludeRelative
                ? new Specification<ShippingEntity>(predicate: o => o.Id == id,
                    includes: source => source
                        .Include(o => o.Order))
                : new Specification<ShippingEntity>(predicate: null, includes: null);

            return await GetSingleAsync(spec);
        }

        public async Task<IEnumerable<ShippingEntity>> GetShippings(bool isIncludeRelative = true)
        {
            ISpecification<ShippingEntity> spec = isIncludeRelative
                ? new Specification<ShippingEntity>(predicate: null,
                    includes: source => source
                        .Include(o => o.Order))
                : new Specification<ShippingEntity>(predicate: null, includes: null);

            return await GetAllAsync(spec);
        }
    }
}
