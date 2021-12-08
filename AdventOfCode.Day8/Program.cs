using AdventOfCode.Day8;

// day 8
SevenSegmentSearch search = new SevenSegmentSearch();
SevenSegmentNote[] input = await search.GetInput();

// part 1
int result = search.CountOutputItemsWithUniqueNumberOfSegments(input);
Console.WriteLine(result);

// part 2
int sum = search.SumOfOutputs(input);
Console.WriteLine(sum);
