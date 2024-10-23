using Domain.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class UserContext(DbContextOptions<UserContext> options, IConfiguration configuration) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultContainer(configuration["CosmoDb:ContainerName"]);
            modelBuilder.HasAutoscaleThroughput(400);

            Configure(modelBuilder.Entity<User>());
        }

        private static void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasNoDiscriminator()
                .HasPartitionKey(x => x.Company)
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasValueGenerator<GuidValueGenerator>();
        }
    }
}
