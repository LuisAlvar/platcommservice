namespace CommandsService.EventProcessing
{
  /// <summary>
  /// Interface containing methods on the core logic on how to handle these RabbitMQ messages.
  /// </summary>
  public interface IEventProcessor
  {
    void ProcessEvent(string message);
  }
}