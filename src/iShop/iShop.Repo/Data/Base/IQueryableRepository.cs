using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using iShop.Common.Helpers;
using iShop.Data.Base;
using iShop.Data.Entities;

namespace iShop.Repo.Data.Base
{
    public interface IQueryableRepository<T>: IDataRepository
        where T : class, IEntityBase
    {
        Task<QueryResult<T>> GetAndFilterAsync(QueryObject queryTerm, bool isIncludeRelative = true);
        Dictionary<string, Expression<Func<Product, object>>> CreateQueryTerms();
    }
}
