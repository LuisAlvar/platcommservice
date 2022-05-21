using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{ 
  /// <summary>
  /// Implementation of ICommandDataClient to send data sync from Platform service to Command service via HttpClient. 
  /// Dependency Injection onto the controller. 
  /// </summary>
  public class HttpCommandDataClient: ICommandDataClient
  {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
      _httpClient = httpClient;
      _configuration = configuration;
    }

    /// <summary>
    /// Async Task Method: Invoke to notify the Command Service when a new platform as been added to Platform Service
    /// </summary>
    /// <param name="plat">A variant of the Platform object</param>
    /// <returns></returns>
    public async Task SendPlatformToCommand(PlatformReadDto plat)
    {
      //Payload
      var httpContent = new StringContent(
        JsonSerializer.Serialize(plat)
        , Encoding.UTF8
        , "application/json");

      var response = await _httpClient.PostAsync(_configuration["CommandService"], httpContent);

      if(response.IsSuccessStatusCode)
      {
        System.Console.WriteLine("---> Sync POST to CommandService was Ok!");
      }
      else
      {
        System.Console.WriteLine("---> Sync POST to CommandService was NOT OK");
      }
    }
  }
}