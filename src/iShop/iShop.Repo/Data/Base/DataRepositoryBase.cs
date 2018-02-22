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


        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec)
        {
            IQueryable<T> query = Context.Set<T>();
            if (spec.Includes != null)
                query = spec.Includes(query);

            if (spec.Predicate != null)
                return await query.Where(spec.Predicate).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<T> GetSingleAsync(ISpecification<T> spec)
        {
            IQueryable<T> query = Context.Set<T>();
            if (spec.Includes != null)
                query = spec.Includes(query);

            if (spec.Predicate != null)
                return await query.SingleOrDefaultAsync(spec.Predicate);

            return await query.SingleOrDefaultAsync();
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

