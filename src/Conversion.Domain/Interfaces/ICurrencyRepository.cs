using Conversion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conversion.Domain.Interfaces
{
    public interface ICurrencyRepository : IRepository<Currency>
    {
        Task<IEnumerable<Currency>> GetByDateAll(DateTime date);
        Task<bool> AddCurrencies(Currency[] currencies);
        Task<bool> IsLatestData(DateTime date);
    }
}
