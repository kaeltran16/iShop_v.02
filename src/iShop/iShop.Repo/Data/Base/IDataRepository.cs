using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Base;

namespace iShop.Repo.Data.Base
{
    public interface IDataRepository { }

    public interface IDataRepository<T> : IDataRepository
        where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec);

        Task<T> GetSingleAsync(ISpecification<T> spec);

        Task<T> AddAsync(T entity);

        void Remove(T entity);

        void Update(T entity);
    }
}
