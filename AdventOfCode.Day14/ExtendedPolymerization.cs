using System.Text;

namespace AdventOfCode.Day14;

public class ExtendedPolymerization
{
    public async Task<(string template, Dictionary<string, char> rules)> GetInput()
    {
        string[] rawInput = await File.ReadAllLinesAsync("input.txt");
        string template = rawInput[0];
        Dictionary<string, char> rules = new Dictionary<string,char>();
        for (int i = 2; i < rawInput.Length; ++i)
        {
            string[] parts = rawInput[i].Split(" -> ");
            rules[parts[0]] = parts[1][0];
        }
        return (template, rules);
    }

    public int GetDiffAfterSteps(
        string template,
        Dictionary<string, char> rules,
        int steps
    )
    {
        for (int i = 0; i < steps; ++i)
        {
            template = ApplyStep(template, rules);
        }

        Dictionary<char, int> counts = template
            .GroupBy(x => x)
            .ToDictionary(k => k.Key, v => v.Count());
        int max = counts.Max(x => x.Value);
        int min = counts.Min(x => x.Value);

        return max - min;
    }

    public string ApplyStep(string template, Dictionary<string, char> rules)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(template[0]);
        for (int i = 0; i < template.Length - 1; ++i)
        {
            string pair = template.Substring(i, 2);
            if (rules.ContainsKey(pair))
            {
                sb.Append(rules[pair]);
            }
            sb.Append(pair[1]);
        }
        return sb.ToString();
    }
}

