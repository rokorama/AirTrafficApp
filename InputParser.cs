using System.Text.RegularExpressions;

public static class InputParser
{
    public static async Task<Flight?> ParseFlightInfoAsync(string input)
    {
        var callsign = Regex.Match(input, @"[A-Z0-9]+").ToString();
        var rawCoordinates = Regex.Match(input, @"\([0-9,-]+\)").ToString();
        var flightCoords = await Task.Run(() => ParseCoordinateString(rawCoordinates));
        if (flightCoords == null)
            return null;
        else
            return new Flight(flightCoords!, callsign);
    }

    public static Point? ParseCoordinateString(string input)
    {
        var parsedCoords = Regex.Replace(input, @"[()]", "").Split(',');
        try
        {
            return new Point(int.Parse(parsedCoords[0]), int.Parse(parsedCoords[1]));
        }
        catch (FormatException)
        {
            return null;
        }
    }
}