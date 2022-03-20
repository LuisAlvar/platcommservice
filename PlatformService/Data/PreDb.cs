using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
  public static class PreDb
  {
    public static void PrepData(IApplicationBuilder app, bool isProduction)
    {
      using (var serviceScope = app.ApplicationServices.CreateScope())
      {
        Migration(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
      }
    }

    private static void Migration(AppDbContext context, bool isProduction)
    {

      if(isProduction)
      {
        System.Console.WriteLine("---> Attempting to Apply Migrations...");
        try
        {
          context.Database.Migrate();
        }
        catch (System.Exception ex)
        {
          System.Console.WriteLine("Error Occur while attempting to apply migrations - " + ex.Message);
        }
      }        

      if (!context.Platform.Any())
      {
        System.Console.WriteLine("---> Seeding Data...");
        context.Platform.AddRange(
          new Platform() {Name="dot net", Publisher="Microsoft", Cost="Free"},
          new Platform() {Name="SQL Server 2019", Publisher="Microsoft", Cost="Free"},
          new Platform() {Name="Kubernetes", Publisher="Cloud native Computing Foundation", Cost="Free"}
        );
        context.SaveChanges();
      }
      else
      {
        System.Console.WriteLine("---> We already have data.");
      }
    }
  }
}