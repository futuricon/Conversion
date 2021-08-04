using Conversion.Domain.Entities;
using System.Threading.Tasks;

namespace Conversion.Domain.Interfaces
{
    public interface IExchangeRepository : IRepository<Exchange>
    {
        Task<bool> AddExchange(Exchange exchange);
    }
}
