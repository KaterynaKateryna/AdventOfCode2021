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

    public int GetDiffAfterStepsGreedy(
        string template,
        Dictionary<string, char> rules,
        int steps
    )
    {
        for (int i = 0; i < steps; ++i)
        {
            template = ApplyStepGreedy(template, rules);
        }

        Dictionary<char, int> counts = template
            .GroupBy(x => x)
            .ToDictionary(k => k.Key, v => v.Count());
        int max = counts.Max(x => x.Value);
        int min = counts.Min(x => x.Value);

        return max - min;
    }

    private string ApplyStepGreedy(string template, Dictionary<string, char> rules)
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

    public long GetDiffAfterStepsOptimized(
        string template,
        Dictionary<string, char> rules,
        int steps
    )
    {
        // initialize pairs count
        Dictionary<string, long> pairsCount = new Dictionary<string, long>();
        for (int i = 0; i < template.Length - 1; ++i)
        {
            string pair = template.Substring(i, 2);
            AddCounts(pairsCount, pair, 1);
        }

        // apply all the steps
        for (int i = 0; i < steps; ++i)
        {
            pairsCount = ApplyStepOptimized(pairsCount, rules);
        }

        // since pairs overlap, ignore the first character of each pair
        Dictionary<char, long> lettersCount = new Dictionary<char, long>();
        foreach (KeyValuePair<string, long> pair in pairsCount)
        {
            AddCounts(lettersCount, pair.Key[1], pair.Value);
        }
        // except the very first character,
        // it shouldn't be ignored
        AddCounts(lettersCount, template[0], 1);

        // get the difference
        long max = lettersCount.Max(x => x.Value);
        long min = lettersCount.Min(x => x.Value);

        return max - min;
    }

    private Dictionary<string, long> ApplyStepOptimized(
        Dictionary<string, long> pairsCount, 
        Dictionary<string, char> rules
    )
    {
        Dictionary<string, long> result = new Dictionary<string, long>();
        foreach (KeyValuePair<string, long> pair in pairsCount)
        {
            if (rules.ContainsKey(pair.Key))
            {
                char newChar = rules[pair.Key];
                string newPairLeft = pair.Key[0].ToString() + newChar.ToString();
                string newPairRight = newChar.ToString() + pair.Key[1].ToString();

                AddCounts(result, newPairLeft, pair.Value);
                AddCounts(result, newPairRight, pair.Value);
            }
            else
            {
                result[pair.Key] = pair.Value;
            }
        }

        return result;
    }

    private void AddCounts<TKey>(Dictionary<TKey, long> items, TKey key, long count)
    {
        if (items.ContainsKey(key))
        {
            items[key] += count;
        }
        else
        {
            items[key] = count;
        }
    }
}

