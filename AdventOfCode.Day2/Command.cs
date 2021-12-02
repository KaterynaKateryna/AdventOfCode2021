namespace AdventOfCode.Day2;

public record Command(Direction Direction, int Value)
{
    public static Command Parse(string input)
    {
        string[] values = input.Split(' ');
        var direction = (Direction)Enum.Parse(typeof(Direction), values[0]);
        int value = int.Parse(values[1]);
        return new Command(direction, value);
    }
}

