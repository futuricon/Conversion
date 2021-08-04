using Conversion.Domain.Entities;
using Conversion.Domain.Interfaces;
using Conversion.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        public Task<bool> AddExchange(Exchange exchange)
        {
            throw new NotImplementedException();
        }
    }
}
