namespace AdventOfCode.Day4;

public class GiantSquid
{
    public async Task<BingoGame> GetInput()
    {
        string[] inputRaw = await File.ReadAllLinesAsync("input.txt");
        return BingoGame.Parse(inputRaw);
    }

    public long GetFinalScoreOfTheFirstWinningBoard(BingoGame game)
    {
        (Board board, int lastNumber) = GetTheFirstWinningBoard(game);
        return GetFinalScore(board, lastNumber);
    }

    public long GetFinalScoreOfTheLastWinningBoard(BingoGame game)
    {
        (Board board, int lastNumber) = GetTheLastWinningBoard(game);
        return GetFinalScore(board, lastNumber);
    }

    private long GetFinalScore(Board board, int lastNumber)
    { 
        int sum = board.Fields
            .SelectMany(r => r.Select(c => c))
            .Where(x => !x.Marked)
            .Sum(x => x.Value);

        return sum * lastNumber;
    }

    private (Board, int) GetTheFirstWinningBoard(BingoGame game)
    {
        for(int i = 0; i < game.Numbers.Length; ++i)
        {
            foreach (Board board in game.Boards)
            { 
                board.MarkNumberOnBoard(game.Numbers[i]);
            }

            if (i >= 4)
            {
                Board? winningBoard = game.Boards.SingleOrDefault(b => b.IsWinningBoard());
                if (winningBoard != null)
                {
                    return (winningBoard, game.Numbers[i]);
                }
            }
        }

        throw new InvalidOperationException("Nobody won");
    }

    private (Board, int) GetTheLastWinningBoard(BingoGame game)
    {
        int lastWinningNumber = -1;
        IEnumerable<Board> lastWinningBoards = new List<Board>();

        for (int i = 0; i < game.Numbers.Length; ++i)
        {
            foreach (Board board in game.Boards)
            {
                board.MarkNumberOnBoard(game.Numbers[i]);
            }

            if (i >= 4)
            {
                var winningBoards = game.Boards.Where(b => b.IsWinningBoard());
                if (winningBoards.Any())
                {
                    lastWinningBoards = winningBoards;
                    lastWinningNumber = game.Numbers[i];

                    game.Boards = game.Boards.Except(winningBoards).ToArray();
                }

                if (!game.Boards.Any())
                {
                    return (lastWinningBoards.First(), lastWinningNumber);
                }
            }
        }

        if (!lastWinningBoards.Any())
        {
            throw new InvalidOperationException("Nobody won");
        }

        return (lastWinningBoards.First(), lastWinningNumber);
    }
}

