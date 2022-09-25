using System.Text.RegularExpressions;

public static class InputParser
{
    public static List<Zone> ReadMap(string filepath)
    {
        var resultList = new List<Zone>();

        var lineArray = File.ReadAllLines(filepath);
        for (int i = 0; i < lineArray.Length; i++)
        {
            resultList.Add(new Zone(lineArray[i]));
        }
        return resultList;
    }

    public static List<Flight> ReadFlights(string input)
    {
        var resultList = new List<Flight>();

        var splitInput = input.Split();
        for (int i = 0; i < splitInput.Length; i++)
        {
            var callsign = Regex.Match(splitInput[i], @"[A-Z0-9]+").ToString();
            var rawCoordinates = Regex.Match(splitInput[i], @"\([0-9,-]+\)").ToString();
            var flightCoords = ParseCoordinateString(rawCoordinates);

            resultList.Add(new Flight(flightCoords, callsign));
        }
        return resultList;
    }

    public static Point ParseCoordinateString(string input)
    {
        var parsedCoords = Regex.Replace(input, "[()]", "").Split(',');
        return new Point(int.Parse(parsedCoords[0]), int.Parse(parsedCoords[1]));
    }
}