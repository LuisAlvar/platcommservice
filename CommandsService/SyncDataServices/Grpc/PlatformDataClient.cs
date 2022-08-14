using System.Collections.Generic;
using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PlatformService;

namespace CommandsService.SyncDataServices.Grpc
{
  /// <summary>
  /// GRPC Service class: Used to fetch all existing Platform records
  /// </summary>
  public class PlatformDataClient : IPlatformDataClient
  {
    /// <summary>
    /// .NET built-in interface for appsetting.json
    /// </summary>
    private IConfiguration _configuration;

    /// <summary>
    /// AutoMapper built-in interface
    /// </summary>
    private IMapper _mapper;

    public PlatformDataClient(IConfiguration configuration, IMapper mapper)
    {
      _configuration = configuration;
      _mapper = mapper;
    }

    /// <summary>
    /// GRPC service call: To fetch all of the Platform records via Platform API.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Platform> ReturnAllPlatforms()
    {
      System.Console.WriteLine($"---> Calling GRPC Service {_configuration["GrpcPlatform"]}");
      var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
      var client = new GrpcPlatform.GrpcPlatformClient(channel);
      var request = new GetAllRequest();

      try
      {
        var response = client.GetAllPlatforms(request);
        return _mapper.Map<IEnumerable<Platform>>(response.Platform);
      }
      catch (System.Exception ex)
      {
        System.Console.WriteLine($"---> Couldnt call Grpc Server {ex.Message}");
        return null;
      }
    }
  }
}