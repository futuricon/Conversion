using Conversion.Domain.Interfaces;
using Conversion.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Conversion.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public IExchangeRepository Exchange { get; private set; }
        public ICurrencyRepository Currency { get; private set; }

        public UnitOfWork(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Exchange = new ExchangeRepository(_context, _logger);
            Currency = new CurrencyRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
