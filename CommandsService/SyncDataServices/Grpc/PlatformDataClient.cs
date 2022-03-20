using System.Collections.Generic;
using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using PlatformService;

namespace CommandsService.SyncDataServices.Grpc
{
  public class PlatformDataClient : IPlatformDataClient
  {
    private IConfiguration _configuration;
    private IMapper _mapper;

    public PlatformDataClient(IConfiguration configuration, IMapper mapper)
    {
      _configuration = configuration;
      _mapper = mapper;
    }

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