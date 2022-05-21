namespace PlatformService.Dtos
{
  /// <summary>
  /// A variant class of Platform, only contains the properties use to read a particular object of this type
  /// </summary>
  public class PlatformReadDto
  {
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string Publisher { get; set; }
    
    public string Cost { get; set; }
  }
}