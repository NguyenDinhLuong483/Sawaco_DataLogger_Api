using Microsoft.EntityFrameworkCore;
using SawacoApi.Domain.Models;
using SawacoApi.Domain.Persistances.Context.Configurations;

namespace SawacoApi.Domain.Persistances.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Logger> Loggers { get; set; }
        public DbSet<StolenLine> StolenLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LoggerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StolenLineEntityTypeConfiguration());
        }
    }
}
