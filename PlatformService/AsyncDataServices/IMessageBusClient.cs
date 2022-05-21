using PlatformService.Dtos;

namespace PlatformService.AsyncDataServices
{
  /// <summary>
  /// Using factory pattern to expose main functionality to send data async from Platform service to Message Queue via RabbitMQ
  /// </summary>
  public interface IMessageBusClient
  {
    /// <summary>
    /// Sending a variant of Platform object to Message Queue via RabbitMQ
    /// </summary>
    /// <param name="platformPublishDto"></param>
    void PublishNewPlatform(PlatformPublishDto platformPublishDto);
  }
}