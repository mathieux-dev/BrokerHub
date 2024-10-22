using BrokerHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrokerHub.Infrastructure.Persistence.Data;

public class BrokerHubDbContext(DbContextOptions<BrokerHubDbContext> options) : DbContext(options)
{
    public DbSet<Imovel> Imoveis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrokerHubDbContext).Assembly);
    }
}