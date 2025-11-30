using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IDestinationRepository : IRepository<Destination>
    {
        Task<IEnumerable<Destination>> SearchByAddress(string address);
    }
}
