namespace CommandsService.Dtos
{
  /// <summary>
  /// A variant class of Platform object used to display read only properties to any external source.
  /// </summary>
  public class PlatformReadDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}