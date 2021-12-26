using AdventOfCode.Day18;

// day 18
Snailfish snailfish = new Snailfish();
string[] input = await snailfish.GetInput();

// part 1
List<SnailfishNumber> numbers = snailfish.ParseSnailfishNumbers(input);
long result = snailfish.GetMagnitudeOfSum(numbers);
Console.WriteLine(result);