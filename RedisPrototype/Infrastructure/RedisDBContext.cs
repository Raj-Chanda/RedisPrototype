using Microsoft.EntityFrameworkCore;
using RedisPrototype.Domain;
using RedisPrototype.Infrastructure.Configuration;

namespace RedisPrototype.Infrastructure
{
    public class RedisDBContext : DbContext
    {
        public RedisDBContext(DbContextOptions<RedisDBContext> options) : base(options) { }

        public DbSet<Truck> Trucks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TruckConfiguration());
        }
    }
}
