namespace AdventOfCode.Day4;

public class BingoGame
{
    public BingoGame(int[] numbers, Board[] boards)
    {
        Numbers = numbers;
        Boards = boards;
    }

    public int[] Numbers { get; }
    public Board[] Boards { get; set; }

    public static BingoGame Parse(string[] input)
    {
        int[] numbers = input[0].Split(',').Select(x => int.Parse(x)).ToArray();

        int numberOfBoards = input.Skip(1).Count() / 6; // empty line and five lines of numbers

        Board[] boards = new Board[numberOfBoards];
        for (int i = 0; i < numberOfBoards; ++i)
        {
            boards[i] = Board.Parse(input.Skip(6 * i + 2).Take(5).ToArray());
        }
        return new BingoGame(numbers, boards);
    }
}

public class Board
{
    public Board(BoardField[][] fields)
    {
        Fields = fields;
    }

    public BoardField[][] Fields { get; }

    public static Board Parse(string[] input)
    {
        int boardSize = input.Length;
        var fields = new BoardField[boardSize][];

        for (int i = 0; i < boardSize; i++)
        {
            fields[i] = input[i]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new BoardField(int.Parse(x))).ToArray();
        }

        return new Board(fields);
    }

    public override string ToString()
    {
        var rows = Fields.Select(
            row => string.Join(' ', row.Select(x => x.ToString()))
        );
        return string.Join(Environment.NewLine, rows);
    }

    public void MarkNumberOnBoard(int number)
    {
        for (int i = 0; i < Fields.Length; ++i)
        {
            for (int j = 0; j < Fields.Length; ++j)
            {
                if (Fields[i][j].Value == number)
                {
                    Fields[i][j].Marked = true;
                }
            }
        }
    }

    public bool IsWinningBoard()
    {
        bool hasAWinningRow = Fields.Any(row => row.All(f => f.Marked));
        if (hasAWinningRow)
        {
            return true;
        }

        for (int i = 0; i < Fields[0].Length; i++)
        {
            bool isWinningColumn = Fields.Select(row => row[i]).All(f => f.Marked);
            if (isWinningColumn)
            {
                return true;
            }
        }

        return false;
    }
}

public class BoardField
{
    public BoardField(int value)
    {
        Value = value;
    }

    public int Value { get; }
    public bool Marked { get; set; }

    public override string ToString()
    {
        return Value + (Marked ? "+" : "-");
    }
}
