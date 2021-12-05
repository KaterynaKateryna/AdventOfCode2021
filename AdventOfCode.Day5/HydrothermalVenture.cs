namespace AdventOfCode.Day5;

public class HydrothermalVenture
{
    public async Task<Line[]> GetInput()
    {
        string[] rawInput = await File.ReadAllLinesAsync("input.txt");
        return rawInput.Select(x => Line.Parse(x)).ToArray();
    }

    public int GetCountOfPointsWithOverlappingLines(Line[] lines)
    {
        Dictionary<Point, int> pointsCounts = new Dictionary<Point, int>();
        foreach (Line line in lines)
        {
            if (line.IsVertical())
            {
                int start = line.Start.Y < line.End.Y ? line.Start.Y : line.End.Y;
                int end = line.Start.Y < line.End.Y ? line.End.Y : line.Start.Y;
                for (int i = start; i <= end; ++i)
                {
                    AddLineToPoint(pointsCounts, new Point(line.Start.X, i));
                }
            }
            else if (line.IsHorizontal())
            {
                int start = line.Start.X < line.End.X ? line.Start.X : line.End.X;
                int end = line.Start.X < line.End.X ? line.End.X : line.Start.X;
                for (int i = start; i <= end; ++i)
                {
                    AddLineToPoint(pointsCounts, new Point(i, line.Start.Y));
                }
            }
        }

        return pointsCounts.Count(kv => kv.Value > 1);
    }

    private void AddLineToPoint(Dictionary<Point, int> pointsCounts, Point point)
    {
        if (pointsCounts.ContainsKey(point))
        {
            pointsCounts[point]++;
        }
        else 
        {
            pointsCounts[point] = 1;
        }
    }

}

public record Line(Point Start, Point End)
{
    public static Line Parse(string input)
    {
        string[] points = input.Split(" -> ");
        string[] point1 = points[0].Split(",");
        string[] point2 = points[1].Split(",");
        Point p1 = new Point(int.Parse(point1[0]), int.Parse(point1[1]));
        Point p2 = new Point(int.Parse(point2[0]), int.Parse(point2[1]));
        return new Line(p1, p2);
    }

    public bool IsHorizontal()
    {
        return Start.Y == End.Y;
    }

    public bool IsVertical()
    {
        return Start.X == End.X;
    }
}

public record Point(int X, int Y);
