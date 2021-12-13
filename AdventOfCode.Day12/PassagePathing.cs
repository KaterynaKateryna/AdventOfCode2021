using System.Diagnostics;

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
                visited[adjacentCave] = false;
            }
        }
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
