using System.Collections.Generic;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
  [Route("api/c/[controller]")]
  [ApiController]
  public class PlatformsController: ControllerBase
  {
    private readonly ICommandRepo _repo;
    private readonly IMapper _mapper;

    public PlatformsController(ICommandRepo repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;  
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
      System.Console.WriteLine("---> Getting All Platforms from Command Service");
      return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_repo.GetAllPlatforms()));
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
      System.Console.WriteLine("---> Inbound post ## command service");
      return Ok("Inbound test of from Platforms Controller");
    }
  }  
}