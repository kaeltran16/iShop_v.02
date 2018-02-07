using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;

namespace iShop.Repo.Data.Implementations
{
    public class SupplierRepository: DataRepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await GetAllAsync();
        }

        public async Task<Supplier> GetSupplier(Guid supplierId)
        {
            return await GetSingleAsync(i => i.Id == supplierId);
        }
    }
}
