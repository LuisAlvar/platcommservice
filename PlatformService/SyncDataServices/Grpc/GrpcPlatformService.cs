using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using PlatformService.Data;

namespace PlatformService.SyncDataServices.Grpc
{
  /// <summary>
  /// Exposing methods for external services via GRPC 
  /// </summary>
  public class GrpcPlatformService: GrpcPlatform.GrpcPlatformBase
  {
    private readonly IPlatformRepo _repo;
    private readonly IMapper _mapper;

    public GrpcPlatformService(IPlatformRepo repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    /// <summary>
    /// Expose a local, in-process function that implements a business operation, Fetch all of the Platform.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
      var response = new PlatformResponse();
      var Platforms = _repo.GetAllPlatforms();

      foreach (var plat in Platforms)
      {
        response.Platform.Add(_mapper.Map<GrpcPlatformModel>(plat));
      }

      return Task.FromResult(response);
    }


  }
}