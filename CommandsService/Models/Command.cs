using System.ComponentModel.DataAnnotations;

namespace CommandsService.Models
{
  /// <summary>
  /// The essential Command class used to create the SQL Table via Entity Framework
  /// </summary>
  public class Command
  {
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string HowTo { get; set; }
    [Required]
    public string CommandLine { get; set; }
    [Required]
    public int PlatformId { get; set; }
    [Required]
    public Platform Platform { get; set; }
  }
}