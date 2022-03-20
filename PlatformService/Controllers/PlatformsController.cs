using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlatformsController: ControllerBase
  {
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;
    private readonly IMessageBusClient _messageBusClient;

    public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient commandDataClient, IMessageBusClient messageBusClient)
    {
      _repository = repository;
      _mapper = mapper;
      _commandDataClient = commandDataClient;
      _messageBusClient = messageBusClient;

    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
      var platformItems = _repository.GetAllPlatforms();
      return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
    }

    [HttpGet("{id}", Name="GetPlatform")]
    public ActionResult<PlatformReadDto> GetPlatform(int id)
    {
      var platformItem = _repository.GetPlatformById(id);
      if (platformItem != null) return Ok(_mapper.Map<PlatformReadDto>(platformItem));
      return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platform)
    {
      var platformModel = _mapper.Map<Platform>(platform);
      _repository.CreatePlatform(platformModel);
      var isSucessfull = _repository.SaveChange();
      var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

      // Send Sync Message
      try
      {
        await _commandDataClient.SendPlatformToCommand(platformReadDto);
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine($"--> Could not send synchronouysly: {ex.Message}");
      }

      // Send Async Message 
      try
      {
        var platformPublishedDto = _mapper.Map<PlatformPublishDto>(platformReadDto);
        platformPublishedDto.Event = "Platform_Published";
        _messageBusClient.PublishNewPlatform(platformPublishedDto);
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine($"--> Could not send asynchronouysly: {ex.Message}");
      }

      return CreatedAtRoute(nameof(GetPlatform), new { Id = platformReadDto.Id }, platformReadDto);
    }

  }
}