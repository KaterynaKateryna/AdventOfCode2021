using AdventOfCode.Day6;

// day 6
Lanternfish lanternfish = new Lanternfish();
List<int> input = await lanternfish.GetInput();

// part 1
int numberOfFish = lanternfish.CountFishAfterDays(input, 80);
Console.WriteLine(numberOfFish);
