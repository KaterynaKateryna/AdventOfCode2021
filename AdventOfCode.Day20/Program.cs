using AdventOfCode.Day20;

// day 20
TrenchMap trenchMap = new TrenchMap();
(bool[] algorithm, bool[][] image) = await trenchMap.GetInput();

// part 1
long count = trenchMap.GetLitPixelsAfterEnhancements(algorithm, image, 2);
Console.WriteLine(count);

// part 2
long count2 = trenchMap.GetLitPixelsAfterEnhancements(algorithm, image, 50);
Console.WriteLine(count2);
