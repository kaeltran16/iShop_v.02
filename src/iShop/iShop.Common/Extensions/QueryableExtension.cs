using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iShop.Common.Helpers;

namespace iShop.Common.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, QueryObject queryTerm)
        {
            queryTerm.Page = queryTerm.Page.SetBackValueWhenExtended(ApplicationConstants.QueryTerm.MinPage,
                ApplicationConstants.QueryTerm.MaxPage, ApplicationConstants.QueryTerm.MinPage);
            queryTerm.PageSize = queryTerm.PageSize.SetBackValueWhenExtended(ApplicationConstants.QueryTerm.MinPageSize,
                ApplicationConstants.QueryTerm.MaxPageSize, ApplicationConstants.QueryTerm.MaxPageSize);

            return query.Skip((queryTerm.Page - 1) * queryTerm.PageSize).Take(queryTerm.PageSize);
        }

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, QueryObject queryTerm, Dictionary<string, Expression<Func<T, object>>> columnMap)
        {
            if (string.IsNullOrWhiteSpace(queryTerm.SortBy) || !columnMap.ContainsKey(queryTerm.SortBy))
                return query;
            query = queryTerm.IsAscending
                ? query.OrderBy(columnMap[queryTerm.SortBy])
                : query.OrderByDescending(columnMap[queryTerm.SortBy]);
            return query;
        }
    }
}
