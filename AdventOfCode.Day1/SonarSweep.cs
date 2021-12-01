namespace AdventOfCode.Day1;

public class SonarSweep
{
    public async Task<int[]> GetInput()
    {
        string[] inputRaw = await File.ReadAllLinesAsync("input.txt");
        return inputRaw.Select(x => int.Parse(x)).ToArray();
    }

    public long CountIncreases(int[] input)
    {
        long increases = 0;
        input.Aggregate((current, next) =>
        {
            if (next > current)
            { 
                increases++; 
            }
            return next;
        });
        return increases;
    }

    public long CoundIncreasesOfMeasurementWindows(int[] input)
    {
        int[] measurementWindows = GetMeasurementWindows(input, 3);
        return CountIncreases(measurementWindows);
    }

    private int[] GetMeasurementWindows(int[] input, int count)
    {
        int[] measurementWindows = new int[input.Length - (count - 1)];
        for (int i = 0; i < measurementWindows.Length; ++i)
        {
            int windowSum = 0;
            for (int j = 0; j < count; ++j)
            {
                windowSum += input[i + j];
            }
            measurementWindows[i] = windowSum;
        }
        return measurementWindows;
    }
}
