//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using iShop.Data.Entities;
//using iShop.Repo.Data.Base;
//using iShop.Repo.Data.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace iShop.Repo.Data.Implementations
//{
//    public class ShoppingCartRepository : DataRepositoryBase<ShoppingCart>, IShoppingCartRepository
//    {

//        public ShoppingCartRepository(ApplicationDbContext context)
//            : base(context)
//        {
//        }

//        public async Task<IEnumerable<ShoppingCart>> GetUserShoppingCarts(Guid userId, bool isIncludeRelative = true)
//        {
//            ISpecification<ShoppingCart> spec = isIncludeRelative
//                ? new Specification<ShoppingCart>(predicate: p => p.UserId == userId,
//                    includes: source => source
//                        .Include(c => c.Carts)
//                        .ThenInclude(p => p.Product)
//                        .Include(u => u.User))
//                : new Specification<ShoppingCart>(predicate: o => o.UserId == userId,
//                    includes: null);

//            return await GetAllAsync(spec);
//        }

//        public async Task<ShoppingCart> GetShoppingCart(Guid id, bool isIncludeRelative = true)
//        {
//            ISpecification<ShoppingCart> spec = isIncludeRelative
//                ? new Specification<ShoppingCart>(predicate: p => p.Id == id,
//                    includes: source => source
//                        .Include(c => c.Carts)
//                        .ThenInclude(p => p.Product)
//                        .Include(u => u.User))
//                : new Specification<ShoppingCart>(predicate: o => o.Id == id,
//                    includes: null);

//            return await GetSingleAsync(spec);
//        }

//        public async Task<IEnumerable<ShoppingCart>> GetShoppingCarts(bool isIncludeRelative = true)
//        {
//            ISpecification<ShoppingCart> spec = isIncludeRelative
//                ? new Specification<ShoppingCart>(predicate: null,
//                    includes: source => source
//                        .Include(c => c.Carts)
//                        .ThenInclude(p => p.Product)
//                        .Include(u => u.User))
//                : new Specification<ShoppingCart>(predicate: null,
//                    includes: null);

//            return await GetAllAsync(spec);
//        }
//    }
//}
