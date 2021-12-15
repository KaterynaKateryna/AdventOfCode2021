using AdventOfCode.Day14;

// day 14
ExtendedPolymerization extendedPolymerization = new ExtendedPolymerization();
(string template, Dictionary<string, char> rules) = await extendedPolymerization.GetInput();

// part 1
int result = extendedPolymerization.GetDiffAfterSteps(template, rules, 10);
Console.WriteLine(result);
