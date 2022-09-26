public class Zone
{
    public string DangerLevel { get; set; } = null!;
    public IShape Shape { get; set; } = null!;

    public Zone(string inputLine)
    {
        var args = inputLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        DangerLevel = args[0];

        var shapeName = args[1];
        if (shapeName == "rectangle")
        {
            var topLeftPoint = InputParser.ParseCoordinateString(args[3]);
            var bottomRightPoint = InputParser.ParseCoordinateString(args[4]);
            if (topLeftPoint == null || bottomRightPoint == null)
                throw new ArgumentException($"The following coordinates could not be parsed: {shapeName}");
            Shape = new Rectangle(topLeftPoint, bottomRightPoint);
        }
        else if (shapeName == "circle")
        {
            var centerPoint = InputParser.ParseCoordinateString(args[3]);
            var radius = int.Parse(args[4]);
            if (centerPoint == null)
                throw new ArgumentException($"The following coordinates could not be parsed: {shapeName}");
            Shape = new Circle(centerPoint, radius);
        }
        else
            throw new ArgumentException($"The following shape name was not recognized: {shapeName}");
    }

    public bool Contains(Point point)
    {
        return Shape.Contains(point);
    }
}