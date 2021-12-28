using System.Diagnostics;

namespace AdventOfCode.Day19;
    
public class BeaconScanner
{
    public async Task<List<Scanner>> GetInput()
    {
        return GetInput(await File.ReadAllTextAsync("input.txt"));
    }

    public List<Scanner> GetInput(string input)
    {
        string[] lines = input.Split('\n');

        List<Scanner> scanners = new List<Scanner>();
        Scanner current = null;
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (current != null)
                {
                    scanners.Add(current);
                }
            }
            else if (line.Contains("scanner"))
            {
                string id = line.Replace("--- scanner ", "").Replace(" ---", "");
                current = new Scanner() { ScannerId = int.Parse(id) };
            }
            else
            {
                string[] parts = line.Split(',');
                current.Beacons.Add(new Coordinates(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
            }
        }

        return scanners;
    }

    public void CalculateScannerCoordinates(List<Scanner> scanners)
    {
        foreach (Scanner scanner in scanners)
        {
            scanner.BeaconRotations = scanner.GetRotations();
            scanner.RelativeCoordinates = scanner.GetRelativeCoordinates();
        }

        scanners[0].Position = new Coordinates(0, 0, 0);
        Queue<Scanner> ones = new Queue<Scanner>();
        ones.Enqueue(scanners[0]);
        while (scanners.Any(s => s.Position == null))
        {
            Scanner one = ones.Dequeue();
            foreach (Scanner other in scanners.Where(s => s.Position == null))
            {
                (bool overlap, int oneIndex, int otherIndex, List<Coordinates> oneMatch, List<Coordinates> otherMatch) = Overlap(one, other);
                if (overlap)
                {
                    other.Position = Coordinates.GetDifference(oneMatch[oneIndex], otherMatch[otherIndex]);

                    other.Beacons = otherMatch.Select(c => Coordinates.Add(other.Position, c)).ToList();
                    other.BeaconRotations = other.GetRotations();
                    other.RelativeCoordinates = other.GetRelativeCoordinates();

                    ones.Enqueue(other);
                }
            }
        }
    }

    public long GetBeaconCount(List<Scanner> scanners)
    {
        return scanners.SelectMany(s => s.Beacons).Distinct().LongCount();
    }

    public long GetMaxManhattanDistance(List<Scanner> scanners)
    {
        List<Coordinates> positions = scanners.Select(x => x.Position).ToList();

        long max = -1L;
        for (int i = 0; i < positions.Count-1; ++i)
        {
            for (int j = i+1; j < positions.Count; ++j)
            {
                long distance = GetManhattanDistance(positions[i], positions[j]);
                if (distance > max)
                { 
                    max = distance;
                }
            }
        }
        return max;
    }

    public (bool overlap, int oneIndex, int otherIndex, List<Coordinates> one, List<Coordinates> other) Overlap(Scanner one, Scanner other)
    {
        for (int i = 0; i < one.RelativeCoordinates.Count; ++i)
        {
            for (int j = 0; j < other.RelativeCoordinates.Count; ++j)
            {
                for (int k = 0; k < one.RelativeCoordinates[i].Count; ++k)
                {
                    for (int l = 0; l < other.RelativeCoordinates[j].Count; ++l)
                    {
                        var matches = one.RelativeCoordinates[i][k].Intersect(other.RelativeCoordinates[j][l]).ToList();

                        if (matches.Count() >= 12)
                        {
                            return (true, k, l, one.BeaconRotations[i], other.BeaconRotations[j]);
                        }
                    }
                }
            }
        }

        return (false, -1, -1, null, null);
    }

    public long GetManhattanDistance(Coordinates one, Coordinates other)
    { 
        return Math.Abs(one.X - other.X) + Math.Abs(one.Y - other.Y) + Math.Abs(one.Z - other.Z);
    }
}

public record Coordinates(int X, int Y, int Z)
{
    public static Coordinates GetDifference(Coordinates one, Coordinates other)
    {
        return new Coordinates(one.X - other.X, one.Y - other.Y, one.Z - other.Z);
    }

    public static Coordinates Add(Coordinates one, Coordinates other)
    {
        return new Coordinates(one.X + other.X, one.Y + other.Y, one.Z + other.Z);
    }
}

public class Scanner
{
    public Scanner()
    {
        Beacons = new List<Coordinates>();
    }

    public int ScannerId { get; set; }

    public Coordinates Position { get; set; }

    public List<Coordinates> Beacons { get; set; }

    public List<List<Coordinates>> BeaconRotations { get; set; }

    public List<List<List<Coordinates>>> RelativeCoordinates { get; set; }

    public List<List<Coordinates>> GetRotations()
    {
        List<List<Coordinates>> rotations = new List<List<Coordinates>>();

        var facingX = Beacons.Select(point => new Coordinates(point.X, point.Y, point.Z)).ToList();
        rotations.AddRange(GetRotationsOfOnePosition(facingX));

        var facingMinusX = Beacons.Select(point => new Coordinates(-point.X, point.Y, -point.Z)).ToList();
        rotations.AddRange(GetRotationsOfOnePosition(facingMinusX));

        var facingY = Beacons.Select(point => new Coordinates(point.Y, -point.X, point.Z)).ToList();
        rotations.AddRange(GetRotationsOfOnePosition(facingY));

        var facingMinusY = Beacons.Select(point => new Coordinates(-point.Y, point.X, point.Z)).ToList();
        rotations.AddRange(GetRotationsOfOnePosition(facingMinusY));

        var facingZ = Beacons.Select(point => new Coordinates(point.Z, point.Y, -point.X)).ToList();
        rotations.AddRange(GetRotationsOfOnePosition(facingZ));

        var facingMinusZ = Beacons.Select(point => new Coordinates(-point.Z, point.Y, point.X)).ToList();
        rotations.AddRange(GetRotationsOfOnePosition(facingMinusZ));

        return rotations;
    }

    public List<List<List<Coordinates>>> GetRelativeCoordinates()
    {
        List<List<List<Coordinates>>> result = new List<List<List<Coordinates>>>();

        foreach (List<Coordinates> oneRotation in BeaconRotations)
        {
            List<List<Coordinates>> rotationRelativeCoordinates = new List<List<Coordinates>>();
            for (int i = 0; i < oneRotation.Count; ++i)
            {
                List<Coordinates> relativeCoordinates = new List<Coordinates>();
                for (int j = 0; j < oneRotation.Count; ++j)
                {
                    relativeCoordinates.Add(Coordinates.GetDifference(oneRotation[i], oneRotation[j]));
                }
                rotationRelativeCoordinates.Add(relativeCoordinates);
            }
            result.Add(rotationRelativeCoordinates);
        }
        return result;
    }

    private List<List<Coordinates>> GetRotationsOfOnePosition(List<Coordinates> coordinates)
    {
        List<List<Coordinates>> rotations = new List<List<Coordinates>>();

        var rotation1 = coordinates.Select(point => new Coordinates(point.X, point.Y, point.Z)).ToList();
        rotations.Add(rotation1);

        var rotation2 = coordinates.Select(point => new Coordinates(point.X, -point.Z, point.Y)).ToList();
        rotations.Add(rotation2);

        var rotation3 = coordinates.Select(point => new Coordinates(point.X, -point.Y, -point.Z)).ToList();
        rotations.Add(rotation3);

        var rotation4 = coordinates.Select(point => new Coordinates(point.X, point.Z, -point.Y)).ToList();
        rotations.Add(rotation4);

        return rotations;
    }
}
