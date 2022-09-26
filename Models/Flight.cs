public class Flight
{
    public Point Coordinates { get; set; } = new();
    public string Callsign { get; set; } = null!;
    public string DangerLevel { get; set; } = "safe";

    public Flight(Point coordinates, string callsign)
    {
        Coordinates = coordinates;
        Callsign = callsign;
    }
}