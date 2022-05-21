using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos
{
  /// <summary>
  /// A variant class of Platform class, only contains properties need to create a Platform class.
  /// </summary>
  public class PlatformCreateDto
  {
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Publisher { get; set; }
    
    [Required]
    public string Cost { get; set; }
  }
}