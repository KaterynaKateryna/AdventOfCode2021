using AdventOfCode.Day14;

// day 14
ExtendedPolymerization extendedPolymerization = new ExtendedPolymerization();
(string template, Dictionary<string, char> rules) = await extendedPolymerization.GetInput();

// part 1
int result = extendedPolymerization.GetDiffAfterStepsGreedy(template, rules, 10);
Console.WriteLine(result);

// part 2
long result2 = extendedPolymerization.GetDiffAfterStepsOptimized(template, rules, 40);
Console.WriteLine(result2);
