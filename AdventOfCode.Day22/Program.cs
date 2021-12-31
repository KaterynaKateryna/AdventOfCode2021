using AdventOfCode.Day22;

// day 22
ReactorReboot reactorReboot = new ReactorReboot();
Command[] commands = await reactorReboot.GetInput();

// part 1
long result = reactorReboot.GetOnCubes(commands, - 50, 50);
Console.WriteLine(result);

// part 2
long result2 = reactorReboot.GetOnCubesRecursive(commands, int.MinValue, int.MaxValue);
Console.WriteLine(result2);
