namespace CommandsService.Dtos
{
  /// <summary>
  /// A variant of Command object to expose only the properties needed for external sources.
  /// </summary>
  public class CommandReadDto
  {
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string CommandLine { get; set; }
    public int PlatformId { get; set; }
  }
}