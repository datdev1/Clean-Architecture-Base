using Application.Interface.Repository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Comon
{
    public class BaseRepostory<T> : IRepository<T> where T : class
    {
        protected readonly TGSContext _context;

        public BaseRepostory(TGSContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int? pageIndex = null, int? pageSize = null)
        {
            return await _context.Set<T>()
                .Skip(pageIndex.HasValue && pageSize.HasValue ? (pageIndex.Value - 1) * pageSize.Value : 0)
                .Take(pageSize ?? int.MaxValue)
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
