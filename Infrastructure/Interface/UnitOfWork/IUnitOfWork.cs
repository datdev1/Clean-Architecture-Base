using Infrastructure.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDestinationRepository Destinations { get; }

        ICustomerRepository Customers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
