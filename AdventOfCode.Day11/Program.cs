using AdventOfCode.Day11;

// day 11
DumboOctopus dumboOctopus = new DumboOctopus();

// part 1
Octopus[,] input = await dumboOctopus.GetInput();
long count = dumboOctopus.CountHighlightsAfterSteps(input, 100);
Console.WriteLine(count);

// part 2
input = await dumboOctopus.GetInput();
long step = dumboOctopus.FirstStepOctopusesSynchronize(input);
Console.WriteLine(step);
