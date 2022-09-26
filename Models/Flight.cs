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

    public void UpdateFlight(List<Zone> zones)
    {
        bool flightInsideAnyZone = false;
        for (int i = zones.Count; i > 0; i--)
        {
            var currentZone = zones[i - 1];
            if (currentZone.Contains(this.Coordinates))
            {
                flightInsideAnyZone = true;
                if (currentZone.DangerLevel == "safe")
                {
                    if (this.DangerLevel != "safe")
                        this.DangerLevel = "safe";
                    return;
                }
                else if (this.DangerLevel == currentZone.DangerLevel)
                    return;
                else
                {
                    this.DangerLevel = currentZone.DangerLevel;

                    if (currentZone.DangerLevel == "warn")
                        Console.WriteLine($"Warning {this.Callsign}");
                    else if (currentZone.DangerLevel == "fire")
                        Console.WriteLine($"Shooting {this.Callsign} at {this.Coordinates}");
                    return;
                }
            }
        }
        if (flightInsideAnyZone is false)
            this.DangerLevel = "safe";
    }
}