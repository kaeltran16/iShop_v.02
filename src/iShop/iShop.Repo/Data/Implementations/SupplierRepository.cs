using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;

namespace iShop.Repo.Data.Implementations
{
    public class SupplierRepository: DataRepositoryBase<SupplierEntity>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<SupplierEntity>> GetSuppliers()
        {
            var spec = 
                new Specification<SupplierEntity>(predicate: null, includes: null);

            return await GetAllAsync(spec);
        }

        public async Task<SupplierEntity> GetSupplier(Guid supplierId)
        {
            var spec = 
                new Specification<SupplierEntity>(predicate: null, includes: null);

            return await GetSingleAsync(spec);
        }
    }
}
