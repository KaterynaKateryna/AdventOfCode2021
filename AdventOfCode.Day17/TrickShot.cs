namespace AdventOfCode.Day17;

public class TrickShot
{
    public async Task<TargetArea> GetInput()
    {
        string inputRaw = (await File.ReadAllLinesAsync("input.txt"))[0];
        string[] parts = inputRaw.Split("x=");
        string[] coordinates = parts[1].Split(", y=");
        string[] xs = coordinates[0].Split("..");
        string[] ys = coordinates[1].Split("..");

        return new TargetArea(int.Parse(xs[0]), int.Parse(xs[1]), int.Parse(ys[0]), int.Parse(ys[1]));
    }

    public int GetHighestTrajectoryPoint(TargetArea targetArea)
    {
        int x = GetBestX(targetArea);

        int maxResult = 0;
        // brute force, this is not the correct way.
        // there is no guarantee y is within this range
        for (int y = 0; y < 10000; ++y) 
        {
            int? height = GetHighestPointForVelocity(new Velocity(x, y), targetArea);
            if (height.HasValue && height > maxResult)
            {
                maxResult = height.Value;
            }
        }

        return maxResult;
    }

    private int GetBestX(TargetArea targetArea)
    {
        for (int i = 0; i < targetArea.XMax + 1; ++i)
        {
            if (WillXHitArea(i, targetArea))
            {
                return i;
            }
        }

        throw new Exception("There must be at least one x that satisfies the condition");
    }

    private bool WillXHitArea(int x, TargetArea targetArea)
    {
        int distance = x;

        while (x >= 0)
        {
            if (targetArea.XMax < distance)
            {
                return false; // overshot
            }

            if (targetArea.XMin < distance && distance < targetArea.XMax)
            {
                return true; // in the area
            }

            x--;
            distance += x;
        }

        return false; // fell short
    }

    private int? GetHighestPointForVelocity(Velocity velocity, TargetArea targetArea)
    {
        Point point = new Point(0, 0);
        int maxY = 0;
        while (!HasTrajectoryMissedTheArea(point, velocity, targetArea))
        {
            (point, velocity) = PerformStep(point, velocity);
            if (point.Y > maxY)
            {
                maxY = point.Y;
            }
            if (IsPointWithinTargetArea(point, targetArea))
            {
                return maxY;
            }
        }

        return null;
    }

    private bool IsPointWithinTargetArea(Point point, TargetArea targetArea)
    { 
        return 
            point.X >= targetArea.XMin &&
            point.X <= targetArea.XMax &&
            point.Y >= targetArea.YMin &&
            point.Y <= targetArea.YMax;
    }

    private (Point point, Velocity velocity) PerformStep(Point point, Velocity velocity)
    {
        Point newPoint = new Point(point.X + velocity.X, point.Y + velocity.Y);
        int newVelocityX = velocity.X > 0 ? velocity.X - 1 : velocity.X < 0 ? velocity.X + 1 : 0;
        Velocity newVelocity = new Velocity(newVelocityX, velocity.Y - 1);
        return (newPoint, newVelocity);
    }

    private bool HasTrajectoryMissedTheArea(Point point, Velocity velocity, TargetArea targetArea)
    {
        if (point.Y < targetArea.YMin && velocity.Y < 0)
        {
            return true;
        }

        return false;
    }
}

public record TargetArea(int XMin, int XMax, int YMin, int YMax);

public record Point(int X, int Y);

public record Velocity(int X, int Y);

