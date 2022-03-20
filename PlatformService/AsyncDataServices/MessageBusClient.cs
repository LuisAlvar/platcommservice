using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
  public class MessageBusClient : IMessageBusClient
  {
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration configuration)
    {
      _configuration = configuration;

      var factory = new ConnectionFactory(){ 
        HostName = _configuration["RabbitMQHost"]
        , Port = (int.Parse(_configuration["RabbitMQPort"])) 
      };

      try
      {
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

        System.Console.WriteLine("---> Connected to MessageBus");
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine($"---> Could not connect to the Message Bus: {ex.Message}");
      }
    }

    public void PublishNewPlatform(PlatformPublishDto platformPublishDto)
    {
      var message = JsonConvert.SerializeObject(platformPublishDto);
      if(_connection.IsOpen)
      {
        System.Console.WriteLine("---> RabbitMQ Connection Open, sending message ...");
        SendMessage(message);
      }
      else
      {
        System.Console.WriteLine("---> RabbitMQ Connection Closed, not sending...");
      }
    }

    private void SendMessage(string message)
    {
      var body = Encoding.UTF8.GetBytes(message);
      _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);

      System.Console.WriteLine($"---> We have send message({message})");
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
      System.Console.WriteLine("---> RabbitMQ Connection Shutdown");
    }

    public void Dispose()
    {
      System.Console.WriteLine("MessageBus Disposed");
      if (_channel.IsOpen)
      {
        _channel.Close();
        _channel.Dispose();
        _connection.Close();
        _connection.Dispose();
      }
    }
  }
}