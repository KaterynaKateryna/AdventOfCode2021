namespace AdventOfCode.Day15;

public class Chiton
{
    public async Task<int[][]> GetInput()
    {
        string[] inputLines = await File.ReadAllLinesAsync("input.txt");
        return inputLines
            .Select(line => line.Select(x => int.Parse(x.ToString())).ToArray())
            .ToArray();
    }

    public int GetShortestPathGreedy(int[][] input)
    { 
        Dictionary<Point, int> pointShortestPaths = new Dictionary<Point, int>();
        Dictionary<Point, int> distances = new Dictionary<Point, int>();
        for (int i = 0; i < input.Length; ++i)
        {
            for (int j = 0; j < input[0].Length; ++j)
            {
                distances[new Point(i, j)] = int.MaxValue;
            }
        }
        distances[new Point(0, 0)] = 0; // we are here

        while (pointShortestPaths.Count < distances.Count)
        {
            var pointsToConsider = distances.Where(kv => !pointShortestPaths.ContainsKey(kv.Key));
            KeyValuePair<Point, int> min = pointsToConsider.First();
            foreach (KeyValuePair<Point, int> kv in pointsToConsider)
            {
                if (kv.Value < min.Value)
                {
                    min = kv;
                }
            }

            Point currentPoint = min.Key;
            int distanceToCurrent = min.Value;
            int i = currentPoint.I;
            int j = currentPoint.J;

            pointShortestPaths[min.Key] = min.Value;

            if (i > 0 && distanceToCurrent + input[i -1][j] < distances[new Point(i - 1, j)])
            {
                distances[new Point(i - 1, j)] = distanceToCurrent + input[i - 1][j];
            }
            if (j > 0 && distanceToCurrent + input[i][j-1] < distances[new Point(i, j-1)])
            {
                distances[new Point(i, j-1)] = distanceToCurrent + input[i][j-1];
            }
            if (i < input.Length - 1 && distanceToCurrent + input[i + 1][j] < distances[new Point(i + 1, j)])
            {
                distances[new Point(i + 1, j)] = distanceToCurrent + input[i + 1][j];
            }
            if (j < input.Length - 1 && distanceToCurrent + input[i][j + 1] < distances[new Point(i, j + 1)])
            {
                distances[new Point(i, j + 1)] = distanceToCurrent + input[i][j + 1];
            }
        }

        return pointShortestPaths[new Point(input.Length-1, input[0].Length-1)];
    }
}

public record Point(int I, int J);

