namespace PlatformService.Dtos
{
  /// <summary>
  /// A variant class of Platform, only contains the required properties/data needed for other services
  /// </summary>
  public class PlatformPublishDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Event { get; set; }
    
  }

}