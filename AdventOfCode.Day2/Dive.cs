namespace AdventOfCode.Day2;

public class Dive
{
    public async Task<Command[]> GetInput()
    {
        string[] inputRaw = await File.ReadAllLinesAsync("input.txt");
        return inputRaw.Select(x => Command.Parse(x)).ToArray();
    }

    public long GetLocation(Command[] commands)
    {
        long horizontal = 0;
        long vertical = 0;

        foreach (Command command in commands)
        {
            switch (command.Direction)
            {
                case Direction.down:
                    vertical += command.Value;
                    break;
                case Direction.up:
                    vertical -= command.Value;
                    break;
                case Direction.forward:
                    horizontal += command.Value;
                    break;

            }
        }

        return horizontal * vertical;
    }

    public long GetLocationVersion2(Command[] commands)
    {
        long horizontal = 0;
        long vertical = 0;
        long aim = 0;

        foreach (Command command in commands)
        {
            switch (command.Direction)
            {
                case Direction.down:
                    aim += command.Value;
                    break;
                case Direction.up:
                    aim -= command.Value;
                    break;
                case Direction.forward:
                    horizontal += command.Value;
                    vertical += (aim * command.Value);
                    break;

            }
        }

        return horizontal * vertical;
    }
}

