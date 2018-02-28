//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using iShop.Data.Entities;
//using iShop.Repo.Data.Base;
//using iShop.Repo.Data.Interfaces;

//namespace iShop.Repo.Data.Implementations
//{
//    public class SupplierRepository: DataRepositoryBase<Supplier>, ISupplierRepository
//    {
//        public SupplierRepository(ApplicationDbContext context) 
//            : base(context)
//        {
//        }

//        public async Task<IEnumerable<Supplier>> GetSuppliers()
//        {
//            var spec = 
//                new Specification<Supplier>(predicate: null, includes: null);

//            return await GetAllAsync(spec);
//        }

//        public async Task<Supplier> GetSupplier(Guid supplierId)
//        {
//            var spec = 
//                new Specification<Supplier>(predicate: null, includes: null);

//            return await GetSingleAsync(spec);
//        }
//    }
//}
