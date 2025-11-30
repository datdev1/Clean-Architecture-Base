using Application.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDestinationRepository Destinations { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
