namespace AdventOfCode.Day8;

public static class StringExtensions
{
    public static string Sort(this string input)
    {
        return String.Concat(input.OrderBy(x => x));
    }
}
