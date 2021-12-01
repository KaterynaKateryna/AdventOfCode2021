using AdventOfCode.Day1;

// day 1
SonarSweep sonarSweep = new SonarSweep();
var input = await sonarSweep.GetInput();

// part 1
long increasesPart1 = sonarSweep.CountIncreases(input);
Console.WriteLine(increasesPart1);

// part 2
long increasesPart2 = sonarSweep.CoundIncreasesOfMeasurementWindows(input);
Console.WriteLine(increasesPart2);
