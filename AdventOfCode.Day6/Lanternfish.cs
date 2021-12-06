namespace AdventOfCode.Day6;

public  class Lanternfish
{
    public async Task<List<int>> GetInput()
    {
        string inputRaw = await File.ReadAllTextAsync("input.txt");
        return inputRaw
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToList();
    }

    public int CountFishAfterDays(List<int> input, int numberOfDays)
    {
        for (int i = 0; i < numberOfDays; ++i)
        {
            List<int> newFish = new List<int>();
            for (int j = 0; j < input.Count; ++j)
            {
                if (input[j] == 0)
                {
                    newFish.Add(8);
                    input[j] = 6;
                }
                else
                {
                    input[j]--;
                }
            }
            input.AddRange(newFish);
        }
        return input.Count;
    }
}

