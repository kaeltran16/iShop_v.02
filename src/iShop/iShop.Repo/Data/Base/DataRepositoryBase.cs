using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace iShop.Repo.Data.Base
{
    public class DataRepositoryBase<T> : IDataRepository<T>
        where T : class, IEntityBase
    {
        protected ApplicationDbContext Context;

        public DataRepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }


        public IQueryable<T> Get(ISpecification<T> spec)
        {
            IQueryable<T> query = Context.Set<T>();
            if (spec.Includes != null)
                query = spec.Includes(query);

            return spec.Predicate != null ? query.Where(spec.Predicate) : query;
        }
        
        public async Task<T> AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}

