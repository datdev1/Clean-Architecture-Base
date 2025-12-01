using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IBaseService<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(int? pageIndex = null, int? pageSize = null);
        Task AddAsync(T destination);
        Task UpdateAsync(T destination);
        Task DeleteAsync(Guid id);
    }
}
