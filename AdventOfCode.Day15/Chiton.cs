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

    public int[][] GetFullMap(int[][] input)
    {
        int[][] result = new int[input.Length * 5][];
        for (int i = 0; i < result.Length; ++i)
        {
            result[i] = new int[input[0].Length * 5];
            for (int j = 0; j < result[i].Length; ++j)
            {
                int value = input[i % input.Length][j % input[0].Length];
                int shiftedValue = (value + i / input.Length + j / input[0].Length);
                result[i][j] = shiftedValue / 10 == 0 ? shiftedValue : shiftedValue % 10 + 1;
            }
        }
        return result;
    }

    public int GetShortestPathGreedy(int[][] input)
    {
        Dictionary<Point, int> pointShortestPaths = new Dictionary<Point, int>();
        Dictionary<Point, int> distances = new Dictionary<Point, int>();
        HashSet<Point> potentialMinimumPoints = new HashSet<Point>();
        for (int i = 0; i < input.Length; ++i)
        {
            for (int j = 0; j < input[0].Length; ++j)
            {
                Point point = new Point(i, j);
                distances[point] = int.MaxValue;
            }
        }
        Point first = new Point(0, 0);
        distances[first] = 0; // we are here
        potentialMinimumPoints.Add(first);

        while (potentialMinimumPoints.Any())
        {
            Point min = potentialMinimumPoints.First();

            foreach (Point p in potentialMinimumPoints)
            {
                if (distances[p] < distances[min])
                {
                    min = p;
                }
            }

            Point currentPoint = min;
            int distanceToCurrent = distances[min];
            int i = currentPoint.I;
            int j = currentPoint.J;

            pointShortestPaths[currentPoint] = distanceToCurrent;
            potentialMinimumPoints.Remove(currentPoint);

            if (i > 0 && distanceToCurrent + input[i - 1][j] < distances[new Point(i - 1, j)])
            {
                Point adjusted = new Point(i - 1, j);
                distances[adjusted] = distanceToCurrent + input[i - 1][j];
                potentialMinimumPoints.Add(adjusted);
            }
            if (j > 0 && distanceToCurrent + input[i][j - 1] < distances[new Point(i, j - 1)])
            {
                Point adjusted = new Point(i, j - 1);
                distances[adjusted] = distanceToCurrent + input[i][j - 1];
                potentialMinimumPoints.Add(adjusted);
            }
            if (i < input.Length - 1 && distanceToCurrent + input[i + 1][j] < distances[new Point(i + 1, j)])
            {
                Point adjusted = new Point(i + 1, j);
                distances[new Point(i + 1, j)] = distanceToCurrent + input[i + 1][j];
                potentialMinimumPoints.Add(adjusted);
            }
            if (j < input[0].Length - 1 && distanceToCurrent + input[i][j + 1] < distances[new Point(i, j + 1)])
            {
                Point adjusted = new Point(i, j + 1);
                distances[adjusted] = distanceToCurrent + input[i][j + 1];
                potentialMinimumPoints.Add(adjusted);
            }
        }

        return pointShortestPaths[new Point(input.Length - 1, input[0].Length - 1)];
    }
}

public record Point(int I, int J);
