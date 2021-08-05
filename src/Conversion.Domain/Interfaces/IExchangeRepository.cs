using Conversion.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conversion.Domain.Interfaces
{
    public interface IExchangeRepository : IRepository<Exchange>
    {
        Task<IEnumerable<Exchange>> GetSortedExchange(History history);
        Task<bool> AddExchange(Exchange exchange);
    }
}
