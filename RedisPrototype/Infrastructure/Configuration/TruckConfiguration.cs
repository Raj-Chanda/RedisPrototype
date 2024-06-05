using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedisPrototype.Domain;

namespace RedisPrototype.Infrastructure.Configuration
{
    public class TruckConfiguration : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            //builder.ToTable("Truck", "unit");
            builder.ToTable("Truck");
        }
    }
}
