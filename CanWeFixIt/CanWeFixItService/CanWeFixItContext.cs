namespace CanWeFixItService
{
    using Microsoft.EntityFrameworkCore;

    public class CanWeFixItContext : DbContext
    {
        public CanWeFixItContext(DbContextOptions<CanWeFixItContext> options)
            : base(options)
        {
        }

        public DbSet<MarketData> MarketData { get; set; }

        public DbSet<Instrument> Instrument { get; set; }
    }
}
