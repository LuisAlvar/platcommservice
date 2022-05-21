using System.Threading.Tasks;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
  /// <summary>
  /// Using factory pattern to expose the method required to send data sync from Platform service to Command service.
  /// </summary>
  public interface ICommandDataClient
  {
    Task SendPlatformToCommand(PlatformReadDto plat);
  }
}