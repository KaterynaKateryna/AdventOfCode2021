using AdventOfCode.Day7;

// day 7
TheTreacheryOfWhales theTreacheryOfWhales = new TheTreacheryOfWhales();
int[] input = await theTreacheryOfWhales.GetInput();

// part 1
int fuel = theTreacheryOfWhales.GetLowestAlignmentFuel(input);
Console.WriteLine(fuel);
