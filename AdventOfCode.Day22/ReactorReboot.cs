namespace AdventOfCode.Day22;

public class ReactorReboot
{
    public async Task<Command[]> GetInput()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");
        Command[] commands = new Command[lines.Length];
        for (int i = 0; i < lines.Length; ++i)
        {
            string[] parts = lines[i].Split(' ');
            bool on = parts[0] == "on";
            string[] coordinates = parts[1].Split(',');
            string[] xs = coordinates[0].Split('=')[1].Split("..");
            string[] ys = coordinates[1].Split('=')[1].Split("..");
            string[] zs = coordinates[2].Split('=')[1].Split("..");

            commands[i] = new Command(
                on,
                int.Parse(xs[0]),
                int.Parse(xs[1]),
                int.Parse(ys[0]),
                int.Parse(ys[1]),
                int.Parse(zs[0]),
                int.Parse(zs[1])
            );
        }

        return commands;
    }

    public long GetOnCubes(Command[] commands, int lowerLimit, int upperLimit)
    {
        HashSet<Cube> onCubes = new HashSet<Cube>();
        foreach (Command command in commands)
        {
            if (command.IsWithinLimits(lowerLimit, upperLimit))
            {
                ExecuteCommand(command, onCubes);
            }
        }
        return onCubes.LongCount();
    }

    private void ExecuteCommand(Command command, HashSet<Cube> onCubes)
    {
        for (int i = command.XLower; i <= command.XUpper; ++i)
        {
            for (int j = command.YLower; j <= command.YUpper; ++j)
            {
                for (int k = command.ZLower; k <= command.ZUpper; ++k)
                {
                    if (command.On)
                    {
                        onCubes.Add(new Cube(i, j, k));
                    }
                    else
                    { 
                        onCubes.Remove(new Cube(i, j, k));
                    }
                }
            }
        }
    }
}

public record Command(bool On, int XLower, int XUpper, int YLower, int YUpper, int ZLower, int ZUpper)
{
    public bool IsWithinLimits(int lowerLimit, int upperLimit)
    { 
        return 
            XLower >= lowerLimit && XUpper <= upperLimit &&
            YLower >= lowerLimit && YUpper <= upperLimit &&
            ZLower >= lowerLimit && ZUpper <= upperLimit;
    }
}

public record Cube(int X, int Y, int Z);
