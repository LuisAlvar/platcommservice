using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data 
{

  /// <summary>
  /// Implementation of the main functions exposed for the controller.
  /// </summary>
  public class PlatformRepo : IPlatformRepo
  {
    private readonly AppDbContext _context;

    public PlatformRepo(AppDbContext context)
    {
      _context = context;
    }

    public void CreatePlatform(Platform plat)
    {
      if (plat != null)
      {
        _context.Platform.Add(plat);
        
      }else {
        throw new ArgumentNullException(nameof(plat));
      }
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
      return _context.Platform.ToList();
    }

    public Platform GetPlatformById(int id)
    {
      return _context.Platform.FirstOrDefault(pl => pl.Id == id);
    }

    public bool SaveChange()
    {
      return _context.SaveChanges() >= 0;
    }
  }
}
