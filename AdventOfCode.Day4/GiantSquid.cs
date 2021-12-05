namespace AdventOfCode.Day4;

public class GiantSquid
{
    public async Task<BingoGame> GetInput()
    {
        string[] inputRaw = await File.ReadAllLinesAsync("input.txt");
        return BingoGame.Parse(inputRaw);
    }

    public long GetFinalScoreOfTheWinningBoard(BingoGame game)
    {
        (Board board, int lastNumber) = GetTheWinningBoard(game);

        int sum = board.Fields
            .SelectMany(r => r.Select(c => c))
            .Where(x => !x.Marked)
            .Sum(x => x.Value);

        return sum * lastNumber;
    }

    private (Board, int) GetTheWinningBoard(BingoGame game)
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
}

