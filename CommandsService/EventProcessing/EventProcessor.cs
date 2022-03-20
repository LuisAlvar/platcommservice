using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.EventProcessing
{
  public class EventProcessor : IEventProcessor
  {
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
      _mapper = mapper;
      _scopeFactory = scopeFactory;
    }

    public void ProcessEvent(string message)
    {
      var eventType = DetermineEvent(message);
      switch (eventType)
      {
        case EventType.PlatformPublish:
          addPlatform(message);
          break;

        default:
          break;
      }
    }

    private EventType DetermineEvent(string notificationMessage)
    {
      System.Console.WriteLine("---> Determining Event");
      var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

      switch(eventType.Event)
      {
        case "Platform_Published":
          System.Console.WriteLine("--> Platform Published Event Detected");
          return EventType.PlatformPublish;
        default:
          System.Console.WriteLine("---> Could not determine event type");
          return EventType.Undetermined;
      }
    }
    
    private void addPlatform(string platformPublishedMessage)
    {
      using(var scope = _scopeFactory.CreateScope())
      {
        var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
        var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);
        try
        {
          var plat = _mapper.Map<Platform>(platformPublishedDto);
          if(!repo.ExternalPlatformExist(plat.ExternalId))
          {
              repo.CreatePlatform(plat);
              repo.SaveChange();
              System.Console.WriteLine("---> Platform created on Command Service API");
          }
          else 
          {
            System.Console.WriteLine("---> Platform already exists ...");
          }
        }
        catch (System.Exception ex)
        {
          
          System.Console.WriteLine($"--->Could not add Platform to DB {ex.Message}");
        }
      }
    }

  }

  enum EventType
  {
    PlatformPublish,
    Undetermined
  }
}