using AdventOfCode.Day3;
using System.Collections;

// day 3
BinaryDiagnostic diagnostic = new BinaryDiagnostic();
bool[][] input = await diagnostic.GetInput();

// part 1
long powerConsumption = diagnostic.CalculatePowerConsumption(input);
Console.WriteLine(powerConsumption);

// part 2
long lifeSupportRating = diagnostic.CalculateLifeSupportRating(input);
Console.WriteLine(lifeSupportRating);