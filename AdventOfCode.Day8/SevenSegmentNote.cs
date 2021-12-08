namespace AdventOfCode.Day8;

public class SevenSegmentNote
{
    public SevenSegmentNote(string input)
    {
        string[] parts = input.Split('|');
        Patterns = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Output = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }

    public string[] Patterns { get; }

    public string[] Output { get; }
}
