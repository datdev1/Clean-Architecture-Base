using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Infrastructure.Context;
using Infrastructure.Interface.Repository;
using Infrastructure.Repository.Comon;

namespace Infrastructure.Repository
{
    public class DestinationRepository : BaseRepostory<Destination>, IDestinationRepository
    {
        public DestinationRepository(TGSContext context) : base(context)
        {
        }

        public Task<IEnumerable<Destination>> SearchByAddress(string address)
        {
            var results = _context.Destinations
                .Where(d => d.Address.Contains(address))
                .AsEnumerable();
            return Task.FromResult(results);
        }
    }
}
