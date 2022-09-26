public class Map
{
    public List<Zone> Zones { get; set; } = null!;
    public List<Flight> TrackedFlights { get; set; } = null!;

    public Map(string filepath)
    {
        Zones = InputParser.ReadMapFile(filepath)!;
    }

    public void UpdateMap(string input)
    {
        try
        {
            UpdateFlightsData(input);
            Parallel.ForEach(TrackedFlights, flight => UpdateFlight(flight));
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"{ex.Message}: {input}");
        }
    }

    public void UpdateFlightsData(string input)
    {
        var tasks = new List<Task>();

        var updatedFlights = InputParser.ReadFlightsAsync(input);

        TrackedFlights ??= updatedFlights.Result;


        foreach (var flight in updatedFlights.Result)
        {




            tasks.Add(Task.Run(() =>
            {
                var currentFlight = flight;
                // Parallel.ForEach(updatedFlights.Result, currentFlight =>
                // {

                // Check if this flight from the input is already tracked, add to tracked flights
                var index = TrackedFlights.FindIndex(item => item.Callsign == currentFlight.Callsign);
                if (index >= 0)
                    TrackedFlights[index].Coordinates = currentFlight.Coordinates;
                else
                    TrackedFlights.Add(currentFlight);

                // Conversely, remove a tracked flight if it is no longer in the list from input
                bool flightInTrackedList = TrackedFlights.FindIndex(item => item.Callsign == currentFlight.Callsign) >= 0;
                if (flightInTrackedList is false)
                    TrackedFlights.Remove(currentFlight);




            }));
            
        };
    }

    public void UpdateFlight(Flight flight)
    {
        bool flightInsideAnyZone = false;
        for (int i = Zones.Count; i > 0; i--)
        {
            var currentZone = Zones[i - 1];
            if (currentZone.Contains(flight.Coordinates))
            {
                flightInsideAnyZone = true;
                if (currentZone.DangerLevel == "safe")
                {
                    if (flight.DangerLevel != "safe")
                        flight.DangerLevel = "safe";
                    return;
                }
                else if (flight.DangerLevel == currentZone.DangerLevel)
                    return;
                else
                {
                    flight.DangerLevel = currentZone.DangerLevel;

                    if (currentZone.DangerLevel == "warn")
                        Console.WriteLine($"Warning {flight.Callsign}");
                    else if (currentZone.DangerLevel == "fire")
                        Console.WriteLine($"Shooting {flight.Callsign} at {flight.Coordinates}");
                    return;
                }
            }
        }
        if (flightInsideAnyZone is false)
            flight.DangerLevel = "safe";
    }
}
