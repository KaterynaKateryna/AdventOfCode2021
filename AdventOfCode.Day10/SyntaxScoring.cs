namespace AdventOfCode.Day10;

public class SyntaxScoring
{
    public Task<string[]> GetInput()
    {
        return File.ReadAllLinesAsync("input.txt");
    }

    public long GetSyntaxErrorScore(string[] input)
    { 
        var illigalChars = input.Select(x => GetFirstIllegalCharacter(x))
            .Where(c => c.HasValue);

        long result = 0;
        foreach (char c in illigalChars)
        {
            switch (c)
            {
                case ')':
                    result += 3;
                    break;
                case ']':
                    result += 57;
                    break;
                case '}':
                    result += 1197;
                    break;
                case '>':
                    result += 25137;
                    break;
            }    
        }
        return result;
    }

    public long GetMiddleAutocompleteScore(string[] input)
    {
        var autocompletes = input.Select(x => GetAutocompleteCharacters(x))
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(x => GetAutocompleteScore(x))
            .OrderBy(x => x)
            .ToList();

        return autocompletes.Skip(autocompletes.Count / 2).First();
    }

    private char? GetFirstIllegalCharacter(string line)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in line)
        {
            if (IsOpeningBracket(c))
            {
                stack.Push(c);
            }
            else if (AreMatching(stack.Peek(), c))
            {
                stack.Pop();
            }
            else
            {
                return c;
            }
        }

        return null;
    }

    private string GetAutocompleteCharacters(string line)
    {
        Stack<char> stack = new Stack<char>();

        foreach (char c in line)
        {
            if (IsOpeningBracket(c))
            {
                stack.Push(c);
            }
            else if (AreMatching(stack.Peek(), c))
            {
                stack.Pop();
            }
            else
            {
                return string.Empty; // illigal character met
            }
        }

        List<char> result = new List<char>();
        while (stack.Any())
        {
            char opening = stack.Pop();
            char closing = GetMatchingClosing(opening);
            result.Add(closing);
        }
        return new string(result.ToArray());
    }

    private long GetAutocompleteScore(string autocompleteCharacters)
    {
        long result = 0;
        foreach (char c in autocompleteCharacters)
        {
            result *= 5;

            switch (c)
            {
                case ')':
                    result += 1;
                    break;
                case ']':
                    result += 2;
                    break;
                case '}':
                    result += 3;
                    break;
                case '>':
                    result += 4;
                    break;
            }
        }
        return result;
    }

    private bool IsOpeningBracket(char c)
    {
        return c == '[' || c == '(' || c == '{' || c == '<';
    }

    private bool AreMatching(char opening, char closing)
    {
        return (opening == '[' && closing == ']') ||
                (opening == '(' && closing == ')') ||
                (opening == '{' && closing == '}') ||
                (opening == '<' && closing == '>');
    }

    private char GetMatchingClosing(char opening)
    {
        switch (opening)
        {
            case '(':
                return ')';
            case '[':
                return ']';
            case '{':
                return '}';
            case '<':
                return '>';
        }

        throw new InvalidOperationException($"Opening character expected, but met {opening}.");
    }
}
