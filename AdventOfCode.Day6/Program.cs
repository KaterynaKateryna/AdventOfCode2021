using AdventOfCode.Day6;

// day 6
Lanternfish lanternfish = new Lanternfish();
int[] input = await lanternfish.GetInput();

// part 1
int numberOfFish80 = lanternfish.CountFishAfterDaysGreedy(input, 80);
Console.WriteLine(numberOfFish80);

// part 2
long numberOfFish256 = lanternfish.CountFishAfterDaysOptimized(input, 256);
Console.WriteLine(numberOfFish256);
