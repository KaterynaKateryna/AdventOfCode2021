using AdventOfCode.Day2;

// day 2
Dive dive = new Dive();
Command[] commands = await dive.GetInput();

// part 1
long location = dive.GetLocation(commands);
Console.WriteLine(location);

// part 2
long location2 = dive.GetLocationVersion2(commands);
Console.WriteLine(location2);
