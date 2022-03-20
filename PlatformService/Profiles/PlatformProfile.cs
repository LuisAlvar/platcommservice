using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
  public class PlatformsProfile: Profile
  {
    public PlatformsProfile()
    {
      //Source -> Target 
      CreateMap<Platform, PlatformReadDto>();

      //Creation case: 
      CreateMap<PlatformCreateDto, Platform>();

      CreateMap<PlatformReadDto, PlatformPublishDto>();

      //GRPC
      CreateMap<Platform, GrpcPlatformModel>()
        .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id));
    }
  }
}