using AdventOfCode.Day9;

// day 9
SmokeBasin smokeBasin = new SmokeBasin();
int[][] input = await smokeBasin.GetInput();

// part 1
int sum = smokeBasin.SumOfRiskLevels(input);
Console.WriteLine(sum);

// part 2
int product = smokeBasin.ProductOfThreeLargestBasins(input);
Console.WriteLine(product);
