using System;
using System.Collections.Generic;
using System.Linq;
using CommandsService.Models;

namespace CommandsService.Data
{
  public class CommandRepo : ICommandRepo
  {
    private readonly AppDbContext _context;

    public CommandRepo(AppDbContext context)
    {
      _context = context;    
    }

    public void CreateCommand(int platformId, Command command)
    {
      if(command == null)
      {
        throw new ArgumentNullException(nameof(command));
      }
      command.PlatformId = platformId;
      _context.Command.Add(command);
      _context.SaveChanges();
    }

    public void CreatePlatform(Platform plat)
    {
      if(plat != null)
      {
        _context.Platforms.Add(plat);
        _context.SaveChanges();
      }else
      {
        throw new ArgumentNullException(nameof(plat));
      }
    }

    public bool ExternalPlatformExist(int externalPlatformId)
    {
      return _context.Platforms.Any(p => p.ExternalId == externalPlatformId);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
      return _context.Platforms.ToList();
    }

    public Command GetCommand(int platformId, int commandId)
    {
      return _context.Command.Where(c => c.PlatformId == platformId && c.Id == commandId).SingleOrDefault();
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
      return _context.Command.Where(c => c.PlatformId == platformId).OrderBy(c => c.Platform.Name);
    }

    public bool PlaformExits(int platformId)
    {
      return _context.Platforms.Any(p => p.Id == platformId);
    }

    public bool SaveChange()
    {
      return _context.SaveChanges() >= 0;
    }
  }
}