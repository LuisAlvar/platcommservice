using System.Collections.Generic;
using PlatformService.Models;

namespace PlatformService.Data 
{
  /// <summary>
  /// Interface contains the main core methods that will likely be used in the controller.
  /// </summary>
  public interface IPlatformRepo
  {
    bool SaveChange();

    IEnumerable<Platform> GetAllPlatforms();

    Platform GetPlatformById(int id);

    void CreatePlatform(Platform plat);
  }
}