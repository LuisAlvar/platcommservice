using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandsService.EventProcessing
{
  /// <summary>
  /// 
  /// </summary>
  public class EventProcessor : IEventProcessor
  {
    /// <summary>
    /// AutoMapper built-in interface
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// .NET built-in inteface on the apps services 
    /// </summary>
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
      _mapper = mapper;
      _scopeFactory = scopeFactory;
    }

    /// <summary>
    /// Main Method: Depending on the message the function will preform a design task.
    /// </summary>
    /// <param name="message">An incoming message from RabbitMQ.</param>
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

    /// <summary>
    /// Helper Method: Based on the incoming message we will determine a EventType. 
    /// </summary>
    /// <param name="notificationMessage">An incoming message from RabbitMQ</param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Helper Method: A design task to deserialize message to PlatformPublishedDto and mapp object to a Platform, check if platform exits if not then add the platform to the back-end. 
    /// </summary>
    /// <param name="platformPublishedMessage">An income message from RabbitMQ</param>
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

  /// <summary>
  /// The event type we are handling with this class
  /// </summary>
  enum EventType
  {
    PlatformPublish,
    Undetermined
  }
}