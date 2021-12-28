using AdventOfCode.Day19;

// day 19
BeaconScanner beaconScanner = new BeaconScanner();
List<Scanner> scanners = await beaconScanner.GetInput();
beaconScanner.CalculateScannerCoordinates(scanners);

// part 1
long beacons = beaconScanner.GetBeaconCount(scanners);
Console.WriteLine(beacons);

// part 2
long max = beaconScanner.GetMaxManhattanDistance(scanners);
Console.WriteLine(max);