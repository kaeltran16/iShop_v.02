using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Common.Helpers;
using iShop.Data.Base;

namespace iShop.Repo.Data.Base
{
    public interface IQueryableRepository<T>: IDataRepository
        where T : class, IEntityBase
    {
        Task<QueryResult<T>> SortAndFilterAsync(QueryObject queryTerm, bool isIncludeRelative = true);
        Dictionary<string, Expression<Func<T, object>>> CreateQueryTerms();
    }
}
