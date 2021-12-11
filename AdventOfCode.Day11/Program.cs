using AdventOfCode.Day11;

// day 11
DumboOctopus dumboOctopus = new DumboOctopus();
Octopus[,] input = await dumboOctopus.GetInput();

// part 1
long count = dumboOctopus.CountHighlightsAfterSteps(input, 100);
Console.WriteLine(count);
