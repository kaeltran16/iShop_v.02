using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using iShop.Data.Entities;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Repo.Data.Implementations
{
    public class ProductRepository : DataRepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(ApplicationDbContext context)
            : base(context)
        { 
        }

        public override Func<IQueryable<Product>, IIncludableQueryable<Product, object>> CreateInclusiveRelatives()
        {
            return product => product
                .Include(p => p.ProductCategories)
                .ThenInclude(c => c.Category)
                .Include(p => p.Images)
                .Include(p => p.Inventory)
                .ThenInclude(i => i.Supplier);
        }

        public override Dictionary<string, Expression<Func<Product, object>>> CreateQueryTerms()
        {
            var columnMap =
                new Dictionary<string, Expression<Func<Product, object>>>
                {
                    {"name", p => p.Name},
                    {"expired", p => p.ExpiredDate},
                    {"expire", p => p.ExpiredDate},
                    {"stock", p => p.Inventory.Stock},
                    {"price", p => p.Price}
                };
            return columnMap;
        }
    }
}
