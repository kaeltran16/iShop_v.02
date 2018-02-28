using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iShop.Data.Base;

namespace iShop.Repo.Data.Base
{
    public interface IDataRepository { }

    public interface IDataRepository<T> : IDataRepository
        where T : class, IEntityBase
    {
        IQueryable<T> Get(ISpecification<T> spec);
        Task<T> AddAsync(T entity);

        void Remove(T entity);
        void Update(T entity);
    }
}
