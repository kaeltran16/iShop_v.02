using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iShop.Data.Base;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Repo.Data.Base
{
    public interface ISpecification<T>
        where T : class, IEntityBase, new()
    {
        Expression<Func<T, bool>> Predicate { get; }
        Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes { get; }
    }
}
