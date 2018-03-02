using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Repo.Data.Implementations
{
    public class SupplierRepository : DataRepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override Func<IQueryable<Supplier>, IIncludableQueryable<Supplier, object>> CreateInclusiveRelatives()
        {
            return null;
        }

        public override Dictionary<string, Expression<Func<Supplier, object>>> CreateQueryTerms()
        {
            var columnMap =
                new Dictionary<string, Expression<Func<Supplier, object>>>
                {
                    {"name", p => p.Name}
                };
            return columnMap;
        }
    }
}
