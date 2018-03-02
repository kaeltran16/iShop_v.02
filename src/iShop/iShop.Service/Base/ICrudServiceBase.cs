using System.Threading.Tasks;
using iShop.Common.Helpers;
using iShop.Service.Commons;

namespace iShop.Service.Base
{
    public interface ICrudServiceBase<in TDto> where TDto: ISavedBaseDto
    {
        Task<IServiceResult> CreateAsync(TDto dto);
        Task<IServiceResult> GetSingleAsync(string id);
        Task<IServiceResult> GetAllAsync(QueryObject queryTerm);
        Task<IServiceResult> UpdateAsync(string id, TDto dto);
        Task<IServiceResult> RemoveAsync(string id);
    }
}
