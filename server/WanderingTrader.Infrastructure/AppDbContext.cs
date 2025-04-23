
using Microsoft.EntityFrameworkCore;
using WanderingTrader.Core;

namespace WanderingTrader.Infrastructure;

public sealed class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(config =>
        {
            config.ToContainer(nameof(Products));
            config.HasKey(x => x.Id);
            config.HasPartitionKey(x => x.Id);
        });

        modelBuilder.Entity<Order>(config =>
        {
            config.ToContainer(nameof(Orders));
            config.HasKey(x => x.Id);
            config.HasPartitionKey(x => x.Id);
            config.OwnsMany(x => x.Items);
        });
    }
}