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
    public class ShippingRepository : DataRepositoryBase<Shipping>, IShippingRepository
    {
        public ShippingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Func<IQueryable<Shipping>, IIncludableQueryable<Shipping, object>> CreateInclusiveRelatives()
        {
            return shipping => shipping
                .Include(o => o.Order);
        }

        public override Dictionary<string, Expression<Func<Shipping, object>>> CreateQueryTerms()
        {
            var columnMap =
                new Dictionary<string, Expression<Func<Shipping, object>>>
                {
                    {"date", p => p.ShippingDate},
                    {"city", p => p.City},
                    {"name", p => p.UserName}
                };
            return columnMap;
        }
    }
}
