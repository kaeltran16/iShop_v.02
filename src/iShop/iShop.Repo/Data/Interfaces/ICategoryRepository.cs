using iShop.Data.Entities;
using iShop.Repo.Data.Base;

namespace iShop.Repo.Data.Interfaces
{
    public interface ICategoryRepository: IDataRepository<Category>, IQueryableRepository<Category>
    {
       
    }
}
