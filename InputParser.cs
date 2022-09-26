using System.Text.RegularExpressions;

public static class InputParser
{
    public static List<Zone> ReadMapFile(string filepath)
    {
        var fileInfo = new FileInfo(filepath);
        
        if (!fileInfo.Exists)
            throw new FileNotFoundException();
        else if (fileInfo.Extension is not ".map")
            throw new ArgumentException();
        else
        {
            var lines = File.ReadAllLines(filepath);
            return ReadMap(lines);
        }
    }

    public static List<Zone> ReadMap(string[] lines)
    {
        var resultList = new List<Zone>();

        for (int i = 0; i < lines.Length; i++)
        {
            resultList.Add(new Zone(lines[i]));
        }
        return resultList;
    }

    public static async Task<List<Flight>> ReadFlightsAsync(string input)
    {
        var tasks = new List<Task<Flight>>();

        var splitInput = input.Split();
        foreach (var flightInfo in splitInput)
        {
            tasks.Add(ParseFlightInfoAsync(flightInfo));
        }

        var result = await Task.WhenAll<Flight>(tasks);
        return result.ToList();
    }

    public static async Task<Flight> ParseFlightInfoAsync(string input)
    {
        var callsign = Regex.Match(input, @"[A-Z0-9]+").ToString();
        var rawCoordinates = Regex.Match(input, @"\([0-9,-]+\)").ToString();
        var flightCoords = await Task.Run(() => ParseCoordinateString(rawCoordinates));

        return new Flight(flightCoords, callsign);
    }

    public static Point ParseCoordinateString(string input)
    {
        var parsedCoords = Regex.Replace(input, @"[()]", "").Split(',');
        return new Point(int.Parse(parsedCoords[0]), int.Parse(parsedCoords[1]));
    }
}