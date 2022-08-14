using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using PlatformService;

namespace CommandsService.Profiles
{
  /// <summary>
  /// AutoMapper Required: Class that contains all of the explicit mapping between Platform and its like variants. 
  /// </summary>
  public class PlatformProfile: Profile
  {
    public PlatformProfile()
    {
      CreateMap<Platform, PlatformReadDto>();
      CreateMap<PlatformPublishedDto, Platform>()
        .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
      CreateMap<GrpcPlatformModel, Platform>()
        .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.PlatformId));
    }
  }
}