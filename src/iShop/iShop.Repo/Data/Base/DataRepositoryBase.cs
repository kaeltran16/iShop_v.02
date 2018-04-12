using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using iShop.Common.Extensions;
using iShop.Common.Helpers;
using iShop.Data.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace iShop.Repo.Data.Base
{
    public class DataRepositoryBase<T> : IDataRepository<T>, IQueryableRepository<T>
        where T : KeyEntity, IEntityBase
    {
        protected ApplicationDbContext Context;

        public DataRepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<T> GetSingleAsync(Guid id, bool isIncludeRelative = true)
        {
            try
            {
                var includes = CreateInclusiveRelatives();
                var spec = isIncludeRelative
                    ? new Specification<T>(o => o.Id == id, includes)
                    : new Specification<T>(o => o.Id == id, null);

                return await Get(spec).SingleOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool isIncludeRelative = true)
        {
            try
            {
                var includes = CreateInclusiveRelatives();
                var spec = isIncludeRelative
                    ? new Specification<T>(null, includes)
                    : new Specification<T>(null, null);

                return await Get(spec).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await Context.Set<T>().AddAsync(entity);
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public void Remove(T entity)
        {
            try
            {
                Context.Set<T>().Remove(entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public void Update(T entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }       
        }

        protected IQueryable<T> Get(ISpecification<T> spec)
        {
            try
            {
                IQueryable<T> query = Context.Set<T>();
                if (spec.Includes != null)
                    query = spec.Includes(query);

                return spec.Predicate != null ? query.Where(spec.Predicate) : query;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }      
        }

        public async Task<QueryResult<T>> SortAndFilterAsync(QueryObject queryTerm, bool isIncludeRelative = true)
        {
            try
            {
                var includes = CreateInclusiveRelatives();

                var spec = isIncludeRelative
                    ? new Specification<T>(null, includes)
                    : new Specification<T>(null, null);
                var query = Get(spec);

                var columnMap = CreateQueryTerms();
                query = query.ApplyPaging(queryTerm);
                query = query.ApplyOrdering(queryTerm, columnMap);
                var result =
                    new QueryResult<T>() { Items = await query.ToListAsync(), TotalItem = await query.CountAsync() };
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }          
        }

        public virtual Dictionary<string, Expression<Func<T, object>>> CreateQueryTerms()
        {
            throw new Exception("You have to override this function to SORT and FILTER.");
        }

        
        public virtual Func<IQueryable<T>, IIncludableQueryable<T, object>> CreateInclusiveRelatives()
        {
            throw new Exception("You have to override this function perform GET request.");
        }
    }
}

