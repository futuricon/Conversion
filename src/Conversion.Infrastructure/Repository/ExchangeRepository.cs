using Conversion.Domain.Entities;
using Conversion.Domain.Interfaces;
using Conversion.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversion.Infrastructure.Repository
{
    public class ExchangeRepository : Repository<Exchange>, IExchangeRepository
    {
        public ExchangeRepository(
            ApplicationDbContext context,
            ILogger logger
        ) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<Exchange>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(ExchangeRepository));
                return new List<Exchange>();
            }
        }

        public async Task<bool> AddExchange(Exchange exchange)
        {
            await dbSet.AddAsync(exchange);
            return true;
        }

        public async Task<IEnumerable<Exchange>> GetSortedExchange(History history)
        {
            try
            {
                return await dbSet.Where(
                    x=>x.Date.Date == history.Date.Date && x.FromCurrency.Code == history.FromCode && x.ToCurrency.Code == history.ToCode)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get Sorted Exchange method error", typeof(ExchangeRepository));
                return new List<Exchange>();
            }
        }
    }
}
