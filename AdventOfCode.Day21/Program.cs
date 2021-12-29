using AdventOfCode.Day21;

// day 21
DiracDice diracDice = new DiracDice();
(int one, int two) = await diracDice.GetInput();

// part 1
long result = diracDice.GetLosingScoreByNumberOfRolls(one, two);
Console.WriteLine(result);