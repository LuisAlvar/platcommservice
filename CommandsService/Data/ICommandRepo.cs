using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.Data
{
  /// <summary>
  /// Using factory pattern to expose the only DbContext functionality this service will provide access within the controller.
  /// </summary>
  public interface ICommandRepo
  {
    bool SaveChange();

    //Platforms
    IEnumerable<Platform> GetAllPlatforms();
    void CreatePlatform(Platform plat);
    bool PlaformExits(int platformId);
    bool ExternalPlatformExist(int externalPlatformId);

    //Commands
    IEnumerable<Command> GetCommandsForPlatform(int platformId);
    Command GetCommand(int platformId, int commandId);
    void CreateCommand(int platformId, Command command);
  }
}