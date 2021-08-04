using System.Threading.Tasks;

namespace Conversion.Domain.Interfaces
{
    public interface IUnitOfWork 
    {
        IExchangeRepository Exchange { get; }
        ICurrencyRepository Currency { get; }

        Task CompleteAsync();
    }
}
