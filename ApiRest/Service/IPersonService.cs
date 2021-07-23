using ApiRest.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRest.Service
{
    public interface IPersonService
    {
        Task<List<PersonVO>> FindAllAsync();
        Task<PersonVO> FindByIDAsync(long id);
        Task<PersonVO> CreateAsync(PersonVO person);
        Task<PersonVO> UpdateAsync(PersonVO person);
        Task DeleteAsync(long id);
    }
}
