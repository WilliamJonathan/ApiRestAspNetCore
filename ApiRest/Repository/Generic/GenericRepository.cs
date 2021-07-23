using Microsoft.EntityFrameworkCore;
using ApiRest.Model.Base;
using ApiRest.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Aqui faz a persistencia no banco de dados
 */

namespace ApiRest.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly MySQLContext _context;

        private readonly DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public async Task<List<T>> FindAllAsync()
        {
            return await dataset.ToListAsync();
        }

        public async Task<T> FindByIDAsync(long id)
        {
            return await dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<T> CreateAsync(T item)
        {
            try
            {
                dataset.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<T> UpdateAsync(T item)
        {

            var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    await _context.SaveChangesAsync();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteAsync(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /*public async Task<bool> ExistsAsync(long id)
        {
            return await dataset.AnyAsync(p => p.Id.Equals(id));
        }
        */
    }
}
