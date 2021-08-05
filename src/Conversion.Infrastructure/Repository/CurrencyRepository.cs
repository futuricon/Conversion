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

        public async Task<IEnumerable<Currency>> GetByDateAll(DateTime date)
        {
            try
            {
                return await dbSet.Where(x=>x.Date == date).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Get By Date All method error", typeof(CurrencyRepository));
                return new List<Currency>();
            }
        }

        public async Task<bool> IsLatestData(DateTime date)
        {
            try
            {
                //get latest currency object from db
                var dbCurrency = await dbSet.FirstOrDefaultAsync();

                //if db data null it means db is empty
                //anyways we should fill db with data, so we return true
                if (dbCurrency == null)
                    return true;

                //compare date of api & db objects
                int res = DateTime.Compare(date, dbCurrency.Date);

                //return true if the date of the api object is newer
                //than the date of the db object and vice versa 
                return res > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Is Latest Data method error", typeof(CurrencyRepository));
                return true;
            }
        }
    }
}
