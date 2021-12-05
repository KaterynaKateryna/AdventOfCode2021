using AdventOfCode.Day4;

// day 4
GiantSquid giantSquid = new GiantSquid();
BingoGame bingoGame = await giantSquid.GetInput();

// part 1
long scoreFirst = giantSquid.GetFinalScoreOfTheFirstWinningBoard(bingoGame);
Console.WriteLine(scoreFirst);

// part 1
long scoreLast = giantSquid.GetFinalScoreOfTheLastWinningBoard(bingoGame);
Console.WriteLine(scoreLast);
