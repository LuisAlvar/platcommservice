using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.SyncDataServices.Grpc
{
  /// <summary>
  /// Methods exposed for fetching all Platform records form Platform service
  /// </summary>
  public interface IPlatformDataClient
  {
    IEnumerable<Platform> ReturnAllPlatforms();
  }
}