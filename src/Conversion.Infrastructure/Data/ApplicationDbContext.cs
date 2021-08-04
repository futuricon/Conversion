using Conversion.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Conversion.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public virtual DbSet<Currency> Сurrencies { get; set; }
        public virtual DbSet<Exchange> Exchanges { get; set; }
    }
}
