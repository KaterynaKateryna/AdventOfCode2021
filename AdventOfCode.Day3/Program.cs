using AdventOfCode.Day3;
using System.Collections;

// day 3
PowerConsumption powerConsumption = new PowerConsumption();
bool[][] input = await powerConsumption.GetInput();

// part 1
long result = powerConsumption.CalculatePowerConsumption(input);
Console.WriteLine(result);