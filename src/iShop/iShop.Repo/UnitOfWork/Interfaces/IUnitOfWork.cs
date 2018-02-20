using System;
using System.Threading.Tasks;
using iShop.Data.Base;
using iShop.Repo.Data.Base;
using iShop.Repo.Data.Interfaces;

namespace iShop.Repo.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository>()
            where TRepository : IDataRepository;
        Task<bool> CompleteAsync();
    }

}
