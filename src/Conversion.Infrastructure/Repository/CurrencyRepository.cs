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
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(
            ApplicationDbContext context,
            ILogger logger
        ) : base(context, logger)
        { }

        public async Task<bool> AddCurrencies(Currency[] currencies)
        {
            foreach (var entity in currencies)
            {
                await dbSet.AddAsync(entity);
            }
            return true;
        }

        public override async Task<IEnumerable<Currency>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(CurrencyRepository));
                return new List<Currency>();
            }
        }

        public async Task<IEnumerable<Currency>> GetAllByDate(DateTime date)
        {
            try
            {
                return await dbSet.Where(x=>x.Date == date).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(CurrencyRepository));
                return new List<Currency>();
            }
        }
    }
}
