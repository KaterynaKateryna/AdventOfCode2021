namespace AdventOfCode.Day8;

public class SevenSegmentSearch
{
    public async Task<SevenSegmentNote[]> GetInput()
    {
        string[] inputRaw = await File.ReadAllLinesAsync("input.txt");
        return inputRaw.Select(x => new SevenSegmentNote(x)).ToArray();
    }

    public int CountOutputItemsWithUniqueNumberOfSegments(SevenSegmentNote[] input)
    {
        // digit 1 has 2 segments
        // digit 4 has 4 segments
        // digit 7 has 3 segments
        // digit 8 has 7 segments
        int[] uniqueLengths = new int[4] { 2, 3, 4, 7 };
        return input.Sum(x => x.Output.Count(i => uniqueLengths.Contains(i.Length)));
    }
}