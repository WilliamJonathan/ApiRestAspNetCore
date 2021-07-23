using ApiRest.Model;
using ApiRest.Model.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiRest.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> FindAllAsync();
        Task<T> FindByIDAsync(long id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(T item);
        Task DeleteAsync(long id);
        //Task<bool> ExistsAsync(long id);
    }
}
