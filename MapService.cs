public static class MapService
{
    public static List<Zone> ReadMap(string[] lines)
    {
        var resultList = new List<Zone>();

        for (int i = 0; i < lines.Length; i++)
        {
            try
            {
                resultList.Add(new Zone(lines[i]));
            }
            catch (ArgumentException)
            {
                throw new InvalidDataException($"Invalid input at line {i+1} (\"{lines[i]}\". Aborting operation and please doublecheck input.");
            }
        }
        return resultList;
    }

    public static async Task<List<Flight>> ReadFlightsAsync(string input)
    {
        var result = new List<Flight>();
        var splitInput = input.Split(null);
        foreach (var flightInfo in splitInput)
        {
            var parsedInfo = await InputParser.ParseFlightInfoAsync(flightInfo);
            if (parsedInfo is not null)
                result.Add(parsedInfo);
        }
        
        return result;
    }

    public static void UpdateMap(Map map, string input)
    {
        try
        {
            UpdateFlightsData(map, input);
            if (map.TrackedFlights is not null)
                Parallel.ForEach(map.TrackedFlights, flight => flight.UpdateFlight(map.Zones));
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"{ex.Message}: {input}");
        }
    }

    public static void UpdateFlightsData(Map map, string input)
    {
        var tasks = new List<Task>();

        var updatedFlights = MapService.ReadFlightsAsync(input);

        bool updatedInfoIsNull = updatedFlights.Result!.Count == 0;
        if (updatedInfoIsNull)
            return;

        map.TrackedFlights ??= updatedFlights!.Result!;
        foreach (var flight in updatedFlights.Result)
        {
            tasks.Add(Task.Run(() =>
            {
                var currentFlight = flight;

                // Check if this flight from the input is already tracked, add to tracked flights
                var index = map.TrackedFlights.FindIndex(item => item.Callsign == currentFlight!.Callsign);
                if (index >= 0)
                    map.TrackedFlights[index].Coordinates = currentFlight!.Coordinates;
                else
                    map.TrackedFlights.Add(currentFlight!);

                // Conversely, remove a tracked flight if it is no longer in the list from input
                bool flightInTrackedList = map.TrackedFlights.FindIndex(item => item.Callsign == currentFlight!.Callsign) >= 0;
                if (flightInTrackedList is false)
                    map.TrackedFlights.Remove(currentFlight!);
            }));
        };
    }
}