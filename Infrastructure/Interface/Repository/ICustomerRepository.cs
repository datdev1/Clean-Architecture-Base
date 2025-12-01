using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer?> GetByEmailAsync(string email);

    }
}
