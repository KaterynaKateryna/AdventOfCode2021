using System.Text;

namespace AdventOfCode.Day13;

public class TransparentOrigami
{
    public async Task<(List<Point>, List<Fold>)> GetInput()
    {
        string[] inputRaw = await File.ReadAllLinesAsync("input.txt");

        int splitPoint = Array.IndexOf(inputRaw, "");
        List<Point> points = new List<Point>();
        for (int i = 0; i < splitPoint; ++i)
        {
            string[] parts = inputRaw[i].Split(",");
            points.Add(new Point(int.Parse(parts[0]), int.Parse(parts[1])));
        }

        List<Fold> folds = new List<Fold>();
        for (int i = splitPoint+1; i < inputRaw.Length; ++i)
        {
            string[] parts = inputRaw[i].Split(" ")[2].Split("=");
            folds.Add(
                new Fold(parts[0] == "x" ? FoldDirection.X : FoldDirection.Y,
                int.Parse(parts[1]))
            );
        }

        return (points, folds);
    }

    public HashSet<Point> Fold(IEnumerable<Point> points, Fold fold)
    {
        HashSet<Point> result = new HashSet<Point>();
        if (fold.Direction == FoldDirection.X)
        {
            foreach (Point p in points)
            {
                if (p.X < fold.Line)
                {
                    result.Add(p);
                }
                else
                {
                    result.Add(p with { X = fold.Line - (p.X - fold.Line) });
                }
            }
        }
        if (fold.Direction == FoldDirection.Y)
        {
            foreach (Point p in points)
            {
                if (p.Y < fold.Line)
                {
                    result.Add(p);
                }
                else
                {
                    result.Add(p with { Y = fold.Line - (p.Y - fold.Line) });
                }
            }
        }

        return result;
    }

    public HashSet<Point> Fold(IEnumerable<Point> points, IEnumerable<Fold> folds)
    {
        HashSet<Point> result = new HashSet<Point>(points);
        foreach (Fold fold in folds)
        {
            result = Fold(result, fold);
        }
        return result;
    }

    public string ToString(IEnumerable<Point> points)
    {
        int maxX = points.Max(p => p.X);
        int maxY = points.Max(p => p.Y);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i <= maxY; ++i)
        {
            for (int j = 0; j <= maxX; ++j)
            {
                sb.Append(points.Contains(new Point(j, i)) ? "#" : ".");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}

public record Point(int X, int Y);

public record Fold(FoldDirection Direction, int Line);

public enum FoldDirection
{ 
    X =1,
    Y =2
}


