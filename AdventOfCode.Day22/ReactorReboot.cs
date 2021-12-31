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

    public long GetOnCubesRecursive(Command[] commands, int lowerLimit, int upperLimit)
    {
        commands = commands.Where(c => c.IsWithinLimits(lowerLimit, upperLimit)).ToArray();
        return GetOnCubesRecursive(commands, 1, new List<Edge>());
    }

    private long GetOnCubesRecursive(IEnumerable<Command> commands, int dimension, List<Edge> finalEdges)
    {
        if (dimension == 4)
        {
            if (commands.Any() && commands.Last().On)
            {
                long cubes = 1;
                foreach (Edge edge in finalEdges)
                {
                    cubes *= (edge.end - edge.start + 1);
                }
                return cubes;
            }
            return 0;
        }

        List<int> points = commands
            .Select(c => dimension == 1 ? c.XLower : dimension == 2 ? c.YLower : c.ZLower)
            .Union(commands.Select(c => dimension == 1 ? c.XUpper : dimension == 2 ? c.YUpper : c.ZUpper))
            .Distinct()
            .OrderBy(x => x)
            .ToList();
        List<Edge> edges = GetEdges(points);

        Dictionary<Edge, List<Command>> mapped = new Dictionary<Edge, List<Command>>();
        foreach (Edge edge in edges)
        {
            mapped[edge] = new List<Command>();
            foreach (Command command in commands)
            {
                if (Contains(command, edge.start, dimension) && Contains(command, edge.end, dimension))
                {
                    mapped[edge].Add(command);
                }
            }
        }

        long result = 0;
        foreach (KeyValuePair<Edge, List<Command>> kv in mapped)
        {
            if (kv.Value.Any())
            {
                finalEdges.Add(kv.Key);
                long cubes = GetOnCubesRecursive(kv.Value, dimension + 1, finalEdges);
                finalEdges.Remove(kv.Key);
                result += cubes;
            }
        }
        return result;
    }

    private List<Edge> GetEdges(List<int> points)
    {
        List<Edge> edges = new List<Edge>();
        for (int i = 1; i < points.Count; ++i)
        {
            if (i == 1)
            {
                edges.Add(new Edge(points[i - 1], points[i - 1]));
            }
            edges.Add(new Edge(points[i - 1] + 1, points[i] - 1));
            edges.Add(new Edge(points[i], points[i]));
        }
        return edges;
    }

    private bool Contains(Command command, int point, int dimension)
    { 
        switch (dimension)
        {
            case 1:
                return command.XLower <= point && point <= command.XUpper;
            case 2:
                return command.YLower <= point && point <= command.YUpper;
            case 3:
                return command.ZLower <= point && point <= command.ZUpper;
            default:
                throw new Exception("Not valid dimension");
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

public record Edge(int start, int end);
