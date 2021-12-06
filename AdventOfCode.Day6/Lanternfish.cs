namespace AdventOfCode.Day6;

public  class Lanternfish
{
    public async Task<int[]> GetInput()
    {
        string inputRaw = await File.ReadAllTextAsync("input.txt");
        return inputRaw
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToArray();
    }

    public int CountFishAfterDaysGreedy(int[] input, int numberOfDays)
    {
        List<int> result = new List<int>();
        result.AddRange(input);

        for (int i = 0; i < numberOfDays; ++i)
        {
            List<int> newFish = new List<int>();
            for (int j = 0; j < result.Count; ++j)
            {
                if (result[j] == 0)
                {
                    newFish.Add(8);
                    result[j] = 6;
                }
                else
                {
                    result[j]--;
                }
            }
            result.AddRange(newFish);
        }
        return result.Count;
    }

    public long CountFishAfterDaysOptimized(int[] input, int numberOfDays)
    {
        Dictionary<int, long> fishCounts = new Dictionary<int, long>();
        foreach (int fish in input)
        {
            if (fishCounts.ContainsKey(fish))
            {
                fishCounts[fish]++;
            }
            else
            {
                fishCounts[fish] = 1;
            }
        }

        for (int i = 0; i < numberOfDays; ++i)
        {
            long numberOfNewFishToAdd = fishCounts.ContainsKey(0) ? fishCounts[0] : 0;

            for (int j = 0; j < 8; ++j)
            {
                fishCounts[j] = fishCounts.ContainsKey(j+1) ? fishCounts[j+1] : 0;
            }
            fishCounts[8] = numberOfNewFishToAdd;
            fishCounts[6] += numberOfNewFishToAdd;
        }
        return fishCounts.Sum(kv => kv.Value);
    }
}

