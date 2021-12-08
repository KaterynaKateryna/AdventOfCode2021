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

    public int SumOfOutputs(SevenSegmentNote[] input)
    {
        return input.Sum(x => GetOutputValue(x));
    }

    private int GetOutputValue(SevenSegmentNote note)
    {
        string one = note.Patterns.Single(x => x.Length == 2);
        string four = note.Patterns.Single(x => x.Length == 4);
        string seven = note.Patterns.Single(x => x.Length == 3);
        string eight = note.Patterns.Single(x => x.Length == 7);

        var zeroSixNine = note.Patterns.Where(x => x.Length == 6);
        string nine = zeroSixNine.Single(x => four.All(s => x.Contains(s))); 
        string zero = zeroSixNine.Single(x => x != nine && seven.All(s => x.Contains(s)));
        string six = zeroSixNine.Single(x => x != nine && x != zero);

        var twoThreeFive = note.Patterns.Where(x => x.Length == 5);
        string three = twoThreeFive.Single(x => one.All(s => x.Contains(s)));
        string two = twoThreeFive.Single(x => x != three && four.Count(s => x.Contains(s)) == 2);
        string five = twoThreeFive.Single(x => x != three && x != two);

        Dictionary<string, int> digitMapping = new Dictionary<string, int>();
        digitMapping[zero.Sort()] = 0;
        digitMapping[one.Sort()] = 1;
        digitMapping[two.Sort()] = 2;
        digitMapping[three.Sort()] = 3;
        digitMapping[four.Sort()] = 4;
        digitMapping[five.Sort()] = 5;
        digitMapping[six.Sort()] = 6;
        digitMapping[seven.Sort()] = 7;
        digitMapping[eight.Sort()] = 8;
        digitMapping[nine.Sort()] = 9;

        int result = 0;
        for(int i = 0; i < note.Output.Length; ++i)
        {
            string outputDigitSorted = note.Output[i].Sort();
            int digit = digitMapping[outputDigitSorted];
            result += digit * (int)Math.Pow(10, (note.Output.Length - i - 1)); 
        }

        return result;
    }
}