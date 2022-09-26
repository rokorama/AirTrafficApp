public class Map
{
    public List<Zone> Zones { get; set; } = null!;
    public List<Flight> TrackedFlights { get; set; } = null!;

    public Map(string filepath)
    {
        var rawLines = FileReaderService.ReadMapFile(filepath);
        try
        {
            Zones = MapService.ReadMap(rawLines);
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }
}
