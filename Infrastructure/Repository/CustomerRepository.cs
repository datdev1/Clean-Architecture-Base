using Application.Interface.Repository;
using Domain.Model;
using Infrastructure.Context;
using Infrastructure.Repository.Comon;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CustomerRepository : BaseRepostory<Customer>, ICustomerRepository
    {
        public CustomerRepository(TGSContext context) : base(context)
        {
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            var result = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);
            return result;
        }
    }
}
