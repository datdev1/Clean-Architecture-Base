using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer?> GetByEmailAsync(string email);

    }
}
