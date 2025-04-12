using Microsoft.EntityFrameworkCore;
using TurqoiseEatary.Domain.Menu;

namespace TurqoiseEatary.Infrastructure.Persistence;

public class TurqoiseEataryDbContext : DbContext
{
    public TurqoiseEataryDbContext(DbContextOptions<TurqoiseEataryDbContext> options)
    : base(options)
    {
    }
    public DbSet<Menu> Menus { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly
        (typeof(TurqoiseEataryDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}