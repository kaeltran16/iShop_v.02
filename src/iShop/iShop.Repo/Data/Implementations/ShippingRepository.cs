//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using iShop.Data.Entities;
//using iShop.Repo.Data.Base;
//using iShop.Repo.Data.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace iShop.Repo.Data.Implementations
//{
//    public class ShippingRepository: DataRepositoryBase<Shipping>, IShippingRepository
//    {
//        public ShippingRepository(ApplicationDbContext context) : base(context)
//        {
//        }

//        public async Task<Shipping> GetShipping(Guid id, bool isIncludeRelative = true)
//        {
//            ISpecification<Shipping> spec = isIncludeRelative
//                ? new Specification<Shipping>(predicate: o => o.Id == id,
//                    includes: source => source
//                        .Include(o => o.Order))
//                : new Specification<Shipping>(predicate: null, includes: null);

//            return await GetSingleAsync(spec);
//        }

//        public async Task<IEnumerable<Shipping>> GetShippings(bool isIncludeRelative = true)
//        {
//            ISpecification<Shipping> spec = isIncludeRelative
//                ? new Specification<Shipping>(predicate: null,
//                    includes: source => source
//                        .Include(o => o.Order))
//                : new Specification<Shipping>(predicate: null, includes: null);

//            return await GetAllAsync(spec);
//        }
//    }
//}
