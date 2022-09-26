public class Map
{
    public List<Zone> Zones { get; set; } = null!;
    public List<Flight> TrackedFlights { get; set; } = null!;

    public Map(string filepath)
    {
        var rawLines = FileReaderService.ReadMapFile(filepath);
        Zones = MapService.ReadMap(rawLines);
    }
}
