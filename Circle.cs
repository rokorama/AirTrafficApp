public class Circle : IShape
{
    public Point Center { get; set; } = new();
    public double Radius { get; set; }

    public Circle(Point center, int radius)
    {
        Center.X = center.X;
        Center.Y = center.Y;
        Radius = radius;
    }

    public bool Contains(Point point)
    {
        return Math.Pow((point.X - Center.X), 2) + Math.Pow((point.Y - Center.Y), 2) <= Math.Pow(Radius, 2);
    }
}