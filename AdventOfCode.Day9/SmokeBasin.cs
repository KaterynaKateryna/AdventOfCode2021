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
        return GetLowPoints(input).Sum(x => x + 1);
    }

    public List<int> GetLowPoints(int[][] input)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < input.Length; ++i)
        {
            for (int j = 0; j < input[0].Length; ++j)
            {
                if (IsLowPoint(input, i, j))
                { 
                    result.Add(input[i][j]);
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
}
