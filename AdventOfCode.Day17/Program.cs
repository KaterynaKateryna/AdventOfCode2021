using AdventOfCode.Day17;

// day 17
TrickShot trickShot = new TrickShot();
TargetArea targetArea = await trickShot.GetInput();

// part 1
int highest = trickShot.GetHighestTrajectoryPoint(targetArea);
Console.WriteLine(highest);