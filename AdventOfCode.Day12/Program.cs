using AdventOfCode.Day12;

// day 12
PassagePathing passagePathing = new PassagePathing();
string[] input = await passagePathing.GetInput();

// part 1
int result = passagePathing.GetNumberOfPossiblePaths(input);
Console.WriteLine(result);
