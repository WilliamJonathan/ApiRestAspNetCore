using ApiRest.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRest.Service
{
    public interface IBookService
    {
        Task<List<BookVO>> FindAllAsync();
        Task<BookVO> FindByIDAsync(long id);
        Task<BookVO> CreateAsync(BookVO books);
        Task<BookVO> UpdateAsync(BookVO books);
        Task DeleteAsync(long id);
    }
}
