using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data
{
  /// <summary>
  /// The Entity Framework DbContext for this service
  /// </summary>
  public class AppDbContext: DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
    {
      
    }

    public virtual DbSet<Platform> Platforms { get; set; }
    public virtual DbSet<Command> Command { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .Entity<Platform>()
        .HasMany(p => p.Commands)
        .WithOne(c => c.Platform)
        .HasForeignKey(c => c.PlatformId);

      modelBuilder
        .Entity<Command>()
        .HasOne(c => c.Platform)
        .WithMany(p => p.Commands)
        .HasForeignKey(c => c.PlatformId);

    }
  }
}