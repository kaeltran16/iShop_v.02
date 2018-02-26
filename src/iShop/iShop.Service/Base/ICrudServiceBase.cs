using System.Threading.Tasks;
using iShop.Common.Base;
using iShop.Service.Commons;

namespace iShop.Service.Base
{
    public interface ICrudServiceBase<in TDto>
    where TDto: class, ISavedBaseDto
    {
        Task<IServiceResult> CreateAsync(TDto dto);
        Task<IServiceResult> GetSingleAsync(string id);
        Task<IServiceResult> GetAllAsync();
        Task<IServiceResult> UpdateAsync(string id, TDto dto);
        Task<IServiceResult> RemoveAsync(string id);
    }
}
