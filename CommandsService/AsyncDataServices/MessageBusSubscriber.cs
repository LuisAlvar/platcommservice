using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommandsService.EventProcessing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandsService.AsyncDataServices
{
  /// <summary>
  /// A background service - Command service will be listening for any message comming from RabbitMQ Message Queue
  /// </summary>
  public class MessageBusSubscriber : BackgroundService
  {
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
      _configuration = configuration;
      _eventProcessor = eventProcessor;
      
      InitializedRabbitMQ();
    }

    private void InitializedRabbitMQ()
    {
      var factory = new ConnectionFactory(){ 
        HostName = _configuration["RabbitMQHost"]
        , Port = int.Parse(_configuration["RabbitMQPort"])
      };

      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();
      _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

      _queueName = _channel.QueueDeclare().QueueName;
      _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");
      
      System.Console.WriteLine("---> Listening to the Message Bus");

      _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
      System.Console.WriteLine("---> Connection Shutdown");
    }

    /// <summary>
    /// Long running task - it will listen to events from the message bus
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      stoppingToken.ThrowIfCancellationRequested();

      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += (ModuleHandle, ea) =>
      {
        System.Console.WriteLine("---> Event Received!");

        var body = ea.Body;
        var notificationMessage = Encoding.UTF8.GetString(body.ToArray());
        _eventProcessor.ProcessEvent(notificationMessage);
      };

      _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

      return Task.CompletedTask;
    }

    public new void Dispose()
    {
      if(_channel.IsOpen)
      {
        _channel.Close();
        _channel.Dispose();
        _connection.Close();
        _connection.Dispose();
      }
    }

  }
}