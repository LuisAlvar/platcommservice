using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
  /// <summary>
  /// This API's DbContext for creating code-first SQL objects via Entity Framework.
  /// </summary>
  public class AppDbContext: DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
      
    }

    public virtual DbSet<Platform> Platform { get; set; } 

    
  }
}