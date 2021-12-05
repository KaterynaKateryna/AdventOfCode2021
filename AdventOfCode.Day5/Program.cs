using AdventOfCode.Day5;

// day 5
HydrothermalVenture hydrothermalVenture = new HydrothermalVenture();
Line[] lines = await hydrothermalVenture.GetInput();

// part 1
int count = hydrothermalVenture.GetCountOfPointsWithOverlappingLines(lines);
Console.WriteLine(count);

// part 2
int countWithDiagonals = hydrothermalVenture.GetCountOfPointsWithOverlappingLines(lines, includeDiagonal:true);
Console.WriteLine(countWithDiagonals);