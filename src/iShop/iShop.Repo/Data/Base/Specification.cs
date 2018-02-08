using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using iShop.Data.Base;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Repo.Data.Base
{
    public class Specification<T>: ISpecification<T>
        where T : class, IEntityBase, new()
    {
        public Specification(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes)
        {
            Predicate = predicate;
            Includes = includes;
        }

        public Expression<Func<T, bool>> Predicate { get; }
        public Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes { get; }
    }
}
