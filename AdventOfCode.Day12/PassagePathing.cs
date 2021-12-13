namespace AdventOfCode.Day12;

public class PassagePathing
{
    public Task<string[]> GetInput()
    {
        return File.ReadAllLinesAsync("input.txt");
    }

    public int GetNumberOfPossiblePaths(string[] input)
    {
        CaveGraph graph = new CaveGraph(input);
        Dictionary<string, bool> visited = new Dictionary<string, bool>();
        return GetNumberOfPossiblePaths(graph, "start", visited);
    }

    private int GetNumberOfPossiblePaths(CaveGraph graph, string cave, Dictionary<string, bool> visited)
    {
        if (cave == "end")
        {
            return 1;
        }

        if (char.IsLower(cave[0]))
        {
            visited[cave] = true;
        }

        List<string> adjacentCaves = graph.Caves[cave];
        int res = 0;
        foreach (string adjacentCave in adjacentCaves)
        {
            if (!visited.ContainsKey(adjacentCave) || !visited[adjacentCave])
            {
                res += GetNumberOfPossiblePaths(graph, adjacentCave, visited);
            }
        }

        visited[cave] = false;

        return res;
    }

    public int GetNumberOfPossiblePaths2(string[] input)
    {
        CaveGraph graph = new CaveGraph(input);
        Dictionary<string, int> visited = new Dictionary<string, int>();

        int res = GetNumberOfPossiblePaths2(graph, visited, "start");

        return res;
    }

    private int GetNumberOfPossiblePaths2(
        CaveGraph graph,
        Dictionary<string, int> visited,
        string cave
    )
    {
        if (cave == "end")
        {
            return 1;
        }
        if (visited.ContainsKey(cave))
        {
            visited[cave]++;
        }
        else
        {
            visited[cave] = 1;
        }

        List<string> adjacentCaves = graph.Caves[cave];
        int res = 0;
        foreach (string adjacentCave in adjacentCaves)
        {
            if (
                char.IsUpper(adjacentCave[0]) ||
                (
                    char.IsLower(adjacentCave[0]) && 
                    adjacentCave != "start" && 
                    (
                        !visited.ContainsKey(adjacentCave) 
                        || 
                        visited[adjacentCave] == 0
                        ||
                        (!visited.Any(x => char.IsLower(x.Key[0]) && x.Value > 1)))
                    )
                )
            {
                res += GetNumberOfPossiblePaths2(graph, visited, adjacentCave);
            }
        }

        visited[cave]--;

        return res;
    }
}

public class CaveGraph
{
    public Dictionary<string, List<string>> Caves { get; }

    public CaveGraph(string[] input)
    {
        Caves = new Dictionary<string, List<string>>();

        foreach (string inputItem in input)
        {
            string[] caves = inputItem.Split("-");
            AddNode(caves[0], caves[1]);
            AddNode(caves[1], caves[0]);
        }
    }

    private void AddNode(string from, string to)
    {
        if (Caves.ContainsKey(from))
        {
            Caves[from].Add(to);
        }
        else
        {
            Caves[from] = new List<string>() { to };
        }
    }
}
