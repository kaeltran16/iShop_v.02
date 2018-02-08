using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface ISupplierRepository: IDataRepository<SupplierEntity>
    {
        Task<IEnumerable<SupplierEntity>> GetSuppliers();
        Task<SupplierEntity> GetSupplier(Guid supplierId);

    }
}
