public class Map
{
    public List<Zone> Zones { get; set; } = null!;
    public List<Flight> TrackedFlights { get; set; } = null!;

    public void UpdateMap(string input)
    {
        try
        {
            UpdateFlightsData(input);
            foreach (var flight in TrackedFlights)
                UpdateFlight(flight);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"{ex.Message}: {input}");
        }
    }

    public void UpdateFlightsData(string input)
    {
        var updatedFlights = InputParser.ReadFlights(input);

        TrackedFlights ??= updatedFlights;
        for (int i = 0; i < updatedFlights.Count; i++)
        {
            var currentFlight = updatedFlights[i];

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
        }
    }

    public void UpdateFlight(Flight flight)
    {
        bool flightInsideAnyZone = false;
        for (int j = Zones.Count; j > 0; j--)
        {
            var currentZone = Zones[j - 1];

            if (currentZone.Contains(flight.Coordinates))
            {
                flightInsideAnyZone = true;
                if (currentZone.DangerLevel == "safe")
                {
                    if (flight.DangerLevel != "safe")
                        flight.DangerLevel = "safe";
                    return;
                }
                if (flight.DangerLevel == currentZone.DangerLevel)
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