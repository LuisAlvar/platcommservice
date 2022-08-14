using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using PlatformService;

namespace CommandsService.Profiles
{
  /// <summary>
  /// AutoMapper required: Class that contains all of the explicit mapping between Commands and its like variants.
  /// </summary>
  public class CommandsProfile: Profile
  {
    public CommandsProfile()
    {
      CreateMap<CommandCreateDto, Command>();
      CreateMap<Command, CommandReadDto>();
    }
  }
}