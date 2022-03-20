using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos
{
  public class CommandCreateDto
  {
    [Required]
    public string CommandLine { get; set; }
    [Required]
    public string HowTo { get; set; }
  }
}