using AdventOfCode.Day17;

// day 17
TrickShot trickShot = new TrickShot();
TargetArea targetArea = await trickShot.GetInput();

// part 1
int highest = trickShot.GetHighestTrajectoryPoint(targetArea);
Console.WriteLine(highest);

//part 2
int velocities = trickShot.GetAllVelocities(targetArea);
Console.WriteLine(velocities);