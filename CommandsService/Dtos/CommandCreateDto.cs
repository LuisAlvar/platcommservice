using System.ComponentModel.DataAnnotations;

namespace CommandsService.Dtos
{
  /// <summary>
  /// A variant class of Command used to hold the only required needed to creat a Command object.
  /// </summary>
  public class CommandCreateDto
  {
    [Required]
    public string CommandLine { get; set; }
    [Required]
    public string HowTo { get; set; }
  }
}