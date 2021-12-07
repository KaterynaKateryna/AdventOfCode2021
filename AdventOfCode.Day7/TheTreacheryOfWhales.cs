namespace AdventOfCode.Day7;

public class TheTreacheryOfWhales
{
    public async Task<int[]> GetInput()
    {
        string inputRaw = await File.ReadAllTextAsync("input.txt");
        return inputRaw
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToArray();
    }

    public int GetLowestAlignmentFuel(int[] input)
    {
        int fuel = int.MaxValue;

        for (int i = 0; i < input.Length; ++i)
        {
            int fuelCandidate = 0;

            for (int j = 0; j < input.Length; ++j)
            {
                fuelCandidate += Math.Abs(input[j] - input[i]);
            }

            if (fuelCandidate < fuel)
            { 
                fuel = fuelCandidate;
            }
        }

        return fuel;
    }
}
