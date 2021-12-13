using AdventOfCode.Day12;

// day 12
PassagePathing passagePathing = new PassagePathing();
string[] input = await passagePathing.GetInput();

// part 1
int result = passagePathing.GetNumberOfPossiblePaths(input);
Console.WriteLine(result);

// part 1
int result2 = passagePathing.GetNumberOfPossiblePaths2(input);
Console.WriteLine(result2);
