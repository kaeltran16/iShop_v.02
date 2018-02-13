using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Repo.Data.Base;
using iShop.Repo.UnitOfWork.Interfaces;

namespace iShop.Repo.UnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<Type, IDataRepository> _repositories;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, IDataRepository>();
        }


        public TRepository GetRepository<TRepository>() 
            where TRepository : class, IDataRepository
        {
            if (!_repositories.TryGetValue(typeof(TRepository), out var repository))
            {
                repository = (TRepository)Activator.CreateInstance(typeof(TRepository), _context);

                _repositories.Add(typeof(TRepository), repository);
            }

            return (TRepository) repository;
        }

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
