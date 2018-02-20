using System;
using System.Collections.Generic;
using System.Linq;
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
            where TRepository : IDataRepository
        {
            if (!_repositories.TryGetValue(typeof(TRepository), out var repository))
            {
                var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => typeof(TRepository).IsAssignableFrom(p) && !p.IsAbstract);
                var concreteType = types.Single();
                repository = (TRepository)Activator.CreateInstance(concreteType, _context);

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
