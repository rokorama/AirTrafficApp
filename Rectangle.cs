public class Rectangle : IShape
{
    public Point TopLeft { get; set; } = new();
    public Point BottomRight { get; set; } = new();



    public Rectangle(Point point1, Point point2)
    {
            TopLeft.X = point1.X;
            TopLeft.Y = point1.Y;
            BottomRight.X = point2.X;
            BottomRight.Y = point2.Y;
    }

    public bool Contains(Point point)
    {
        return (point.X >= TopLeft.X && point.Y >= TopLeft.Y &&
                point.X <= BottomRight.X && point.Y <= BottomRight.Y)
        ? true : false;
    }
}