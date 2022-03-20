using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
  [Route("api/c/platforms/{platformId}/[controller]")]
  [ApiController]
  public class CommandsController: ControllerBase
  {
    private readonly ICommandRepo _repo;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepo repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
      if (_repo.PlaformExits(platformId))
      {
        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(_repo.GetCommandsForPlatform(platformId)));
      }
      return NotFound();
    }

    [HttpGet("{commandId}", Name="GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
    {
      System.Console.WriteLine($"---> PlatformId received({platformId}) and CommandId received({commandId})");

      if(!_repo.PlaformExits(platformId))
      {
        return NotFound();
      }

      var command = _repo.GetCommand(platformId, commandId);

      if(command == null) return NotFound();

      return _mapper.Map<CommandReadDto>(command);
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto data)
    {
    
      System.Console.WriteLine($"---> PlatformId received({platformId})");

      if(!_repo.PlaformExits(platformId)) return NotFound();

      var commandDb = _mapper.Map<Command>(data);

      _repo.CreateCommand(platformId, commandDb);
      _repo.SaveChange();

      var commandReadDto = _mapper.Map<CommandReadDto>(commandDb);

      return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commandReadDto.Id}, commandReadDto);
    }

  }
}