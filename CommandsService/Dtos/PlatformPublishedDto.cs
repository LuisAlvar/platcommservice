namespace CommandsService.Dtos
{
  /// <summary>
  /// A variant class of Platform used to expose only the required data/properties needed for other internal services.  
  /// </summary>
  public class PlatformPublishedDto
  {
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Event { get; set; }
  }
}