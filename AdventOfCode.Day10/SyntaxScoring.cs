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
}
