namespace AdventOfCode.Day18;

public class Snailfish
{
    public Task<string[]> GetInput()
    {
        return File.ReadAllLinesAsync("input.txt");
    }

    public List<SnailfishNumber> ParseSnailfishNumbers(string[] input)
    {
        return input.Select(x => SnailfishNumber.Parse(x)).ToList();
    }

    public long GetMagnitudeOfSum(List<SnailfishNumber> numbers)
    {
        return Add(numbers).GetMagnitude();
    }

    public SnailfishNumber Add(List<SnailfishNumber> numbers)
    {
        return numbers.Aggregate((one, other) => Add(one, other));
    }

    public SnailfishNumber Add(SnailfishNumber one, SnailfishNumber other)
    {
        SnailfishNumber newNumber = new SnailfishNumber();
        newNumber.Root = new Node();
        newNumber.Root.AddChild(one.Root);
        newNumber.Root.AddChild(other.Root);

        ReduceNumber(newNumber);

        return newNumber;
    }

    public void ReduceNumber(SnailfishNumber number)
    {
        while (true)
        {
            if (Explodes(number))
            {
                continue;
            }
            if (Splits(number))
            {
                continue;
            }
            break;
        }
    }

    public bool Explodes(SnailfishNumber number)
    {
        int level = 0;
        List<(Node, int)> leaves = new List<(Node, int)>();
        number.Root.Visit(leaves, level);

        if (leaves.Any(l => l.Item2 == 5))
        {
            // explode
            int leftIndex = leaves.FindIndex(l => l.Item2 == 5);
            (Node left , _) = leaves[leftIndex]; 
            (Node right, _) = leaves[leftIndex + 1];

            if (leftIndex - 1 >= 0)
            {
                leaves[leftIndex - 1].Item1.Value += left.Value;
            }
            if (leftIndex + 2 < leaves.Count)
            {
                leaves[leftIndex + 2].Item1.Value += right.Value;
            }

            var current = left.Parent;
            current.Left = null;
            current.Right = null;
            current.Value = 0;

            return true;
        }
        return false;
    }

    public bool Splits(SnailfishNumber number)
    {
        int level = 0;
        List<(Node, int)> leaves = new List<(Node, int)>();
        number.Root.Visit(leaves, level);

        if (leaves.Any(l => l.Item1.Value >= 10))
        {
            (Node toSplit, _) = leaves.First(l => l.Item1.Value >= 10);

            int floor = (int)Math.Floor((decimal)toSplit.Value / 2m);
            int ceiling = (int)Math.Ceiling((decimal)toSplit.Value / 2m);

            toSplit.Value = null;
            toSplit.AddChild(new Node { Value = floor });
            toSplit.AddChild(new Node { Value = ceiling });
            return true;
        }
        return false;
    }
}

public class SnailfishNumber
{ 
    public Node Root { get; set; }

    public static SnailfishNumber Parse(string input)
    {
        Node current = null;

        foreach (char c in input)
        {
            switch (c)
            {
                case '[':
                    Node next = new Node();
                    if (current != null)
                    {
                        current.AddChild(next);
                    }
                    current = next;
                    break;
                case ']':
                    if (current.Parent != null)
                    {
                        current = current.Parent;
                    }
                    break;
                case ',':
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    next = new Node();
                    next.Value = int.Parse(c.ToString());
                    current.AddChild(next);
                    break;
            }
        }

        while (current.Parent != null)
        {
            current = current.Parent;
        }

        SnailfishNumber number = new SnailfishNumber();
        number.Root = current;
        return number;
    }

    public override string ToString()
    {
        return Root.ToString();
    }

    public long GetMagnitude()
    {
        return Root.GetMagnitude();
    }
}

public class Node
{ 
    public int? Value { get; set; }

    public Node Parent { get; set; }

    public Node Left { get; set; }

    public Node Right { get; set; }

    public void AddChild(Node child)
    {
        if (Left == null)
        {
            child.Parent = this;
            Left = child;
        }
        else
        {
            child.Parent = this;
            Right = child;
        }
    }

    public void Visit(List<(Node, int)> leaves, int level)
    {
        if (this.Value != null)
        {
            leaves.Add((this, level));
        }

        level++;
        if (Left != null)
        {
            Left.Visit(leaves, level);
        }
        if (Right != null)
        {
            Right.Visit(leaves, level);
        }
    }

    public override string ToString()
    {
        return Value != null ? Value.ToString() : $"[{Left?.ToString()},{Right?.ToString()}]";
    }

    public int GetMagnitude()
    {
        return Value != null ? Value.Value : (3 * Left.GetMagnitude() + 2 * Right.GetMagnitude());
    }
}

