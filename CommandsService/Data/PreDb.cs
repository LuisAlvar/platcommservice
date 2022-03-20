using System.Collections;
using System.Collections.Generic;
using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.Data
{
  public static class PreDb
  {
    public static void InitialMigration(IApplicationBuilder applicationBuilder)
    {
      using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
      {
        var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
        var platforms = grpcClient.ReturnAllPlatforms();
        Migrate(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
      }
    }

    private static void Migrate(ICommandRepo repo, IEnumerable<Platform> platforms)
    {
      System.Console.WriteLine("---> Migrating Platforms Data");

      foreach (var plat in platforms)
      {
        if(!repo.ExternalPlatformExist(plat.ExternalId))
        {
          repo.CreatePlatform(plat);
        }
        repo.SaveChange();
      }

    }
  }
}