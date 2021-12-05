using AdventOfCode.Day4;

// day 4
GiantSquid giantSquid = new GiantSquid();
BingoGame bingoGame = await giantSquid.GetInput();

// part 1
long score = giantSquid.GetFinalScoreOfTheWinningBoard(bingoGame);
Console.WriteLine(score);
