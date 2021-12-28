using AdventOfCode.Day19;

// day 19
BeaconScanner beaconScanner = new BeaconScanner();
List<Scanner> scanners = await beaconScanner.GetInput();

// part 1
long beacons = beaconScanner.GetBeaconCount(scanners);
Console.WriteLine(beacons);