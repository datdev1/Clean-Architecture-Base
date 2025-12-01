using Infrastructure.Context;
using Infrastructure.Interface.Repository;
using Infrastructure.Interface.UnitOfWork;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TGSContext _context;

        public UnitOfWork(TGSContext context)
        {
            _context = context;
            Destinations = new DestinationRepository(_context);
            Customers = new CustomerRepository(_context);
        }

        public IDestinationRepository Destinations { get; }

        public ICustomerRepository Customers { get; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           return await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
