namespace AdventOfCode.Day9;

public class SmokeBasin
{
    public async Task<int[][]> GetInput()
    {
        string[] rawInput = await File.ReadAllLinesAsync("input.txt");
        return rawInput
            .Select(line => line.Select(c => int.Parse(c.ToString())).ToArray())
            .ToArray();
    }

    public int SumOfRiskLevels(int[][] input)
    {
        return GetLowPoints(input).Sum(x => x.Value + 1);
    }

    public int ProductOfThreeLargestBasins(int[][] input)
    {
        return GetLowPoints(input)
            .Select(x => GetBasin(input, x).Count)
            .OrderByDescending(x => x)
            .Take(3)
            .Aggregate((a, b) => a * b);
    }

    private List<Point> GetLowPoints(int[][] input)
    {
        List<Point> result = new List<Point>();
        for (int i = 0; i < input.Length; ++i)
        {
            for (int j = 0; j < input[0].Length; ++j)
            {
                if (IsLowPoint(input, i, j))
                { 
                    result.Add(new Point(i, j, input[i][j]));
                }
            }
        }
        return result;
    }

    private bool IsLowPoint(int[][] input, int i, int j)
    {
        int value = input[i][j];
        int up = i > 0 ? input[i-1][j] : int.MaxValue;
        int left = j > 0 ? input[i][j-1] : int.MaxValue;
        int right = j < (input[0].Length - 1) ? input[i][j+1] : int.MaxValue;
        int down = i < (input.Length - 1) ? input[i+1][j] : int.MaxValue;

        return value < left && value < right && value < up && value < down;
    }

    private HashSet<Point> GetBasin(int[][] input, Point lowPoint)
    {
        HashSet<Point> basin = new HashSet<Point>();
        if (lowPoint.Value == 9)
        { 
            return basin;
        }

        basin.Add(lowPoint);

        // up
        if (lowPoint.I > 0 && input[lowPoint.I - 1][lowPoint.J] > lowPoint.Value)
        {
            basin.UnionWith(GetBasin(input, new Point(lowPoint.I - 1, lowPoint.J, input[lowPoint.I - 1][lowPoint.J])));
        }
        // down
        if (lowPoint.I < (input.Length - 1) && input[lowPoint.I + 1][lowPoint.J] > lowPoint.Value)
        {
            basin.UnionWith(GetBasin(input, new Point(lowPoint.I + 1, lowPoint.J, input[lowPoint.I + 1][lowPoint.J])));
        }
        // left
        if (lowPoint.J > 0 && input[lowPoint.I][lowPoint.J - 1] > lowPoint.Value)
        {
            basin.UnionWith(GetBasin(input, new Point(lowPoint.I, lowPoint.J - 1, input[lowPoint.I][lowPoint.J - 1])));
        }
        // right
        if (lowPoint.J < (input[0].Length - 1) && input[lowPoint.I][lowPoint.J + 1] > lowPoint.Value)
        {
            basin.UnionWith(GetBasin(input, new Point(lowPoint.I, lowPoint.J + 1, input[lowPoint.I][lowPoint.J + 1])));
        }

        return basin;
    }
}

public record Point(int I, int J, int Value);
