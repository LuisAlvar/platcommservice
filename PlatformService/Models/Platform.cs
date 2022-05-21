using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models
{
  /// <summary>
  /// The essential Platform class used to create the SQL Table via Entity Framework
  /// </summary>
  public class Platform
  {
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Publisher { get; set; }
    
    [Required]
    public string Cost { get; set; }
  }  
}