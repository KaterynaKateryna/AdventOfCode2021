using AdventOfCode.Day18;

// day 18
Snailfish snailfish = new Snailfish();
string[] input = await snailfish.GetInput();
List<SnailfishNumber> numbers = snailfish.ParseSnailfishNumbers(input);

// part 1
long result = snailfish.GetMagnitudeOfSum(numbers);
Console.WriteLine(result);

// part 2
long max = snailfish.GetMaxMagnitude(numbers);
Console.WriteLine(max);