using System.Collections.Generic;
using CommandsService.Models;

namespace CommandsService.Data
{
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