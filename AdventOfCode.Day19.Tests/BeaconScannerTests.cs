using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day19.Tests;

public class BeaconScannerTests
{
    [Test]
    public void CalculateScannerCoordinates_should_calculate_coordinates()
    {
        string input = @"
            --- scanner 0 ---
            404,-588,-901
            528,-643,409
            -838,591,734
            390,-675,-793
            -537,-823,-458
            -485,-357,347
            -345,-311,381
            -661,-816,-575
            -876,649,763
            -618,-824,-621
            553,345,-567
            474,580,667
            -447,-329,318
            -584,868,-557
            544,-627,-890
            564,392,-477
            455,729,728
            -892,524,684
            -689,845,-530
            423,-701,434
            7,-33,-71
            630,319,-379
            443,580,662
            -789,900,-551
            459,-707,401

            --- scanner 1 ---
            686,422,578
            605,423,415
            515,917,-361
            -336,658,858
            95,138,22
            -476,619,847
            -340,-569,-846
            567,-361,727
            -460,603,-452
            669,-402,600
            729,430,532
            -500,-761,534
            -322,571,750
            -466,-666,-811
            -429,-592,574
            -355,545,-477
            703,-491,-529
            -328,-685,520
            413,935,-424
            -391,539,-444
            586,-435,557
            -364,-763,-893
            807,-499,-711
            755,-354,-619
            553,889,-390
            ";

        BeaconScanner beaconScanner = new BeaconScanner();
        List<Scanner> scanners = beaconScanner.GetInput(input);
        beaconScanner.CalculateScannerCoordinates(scanners);

        foreach (Scanner scanner in scanners)
        { 
            Assert.IsNotNull(scanner.Position);
        }
    }

    [Test]
    public void CalculateScannerCoordinates_should_calculate_coordinates_2()
    {
        string input = @"
            --- scanner 0 ---
            404,-588,-901
            528,-643,409
            -838,591,734
            390,-675,-793
            -537,-823,-458
            -485,-357,347
            -345,-311,381
            -661,-816,-575
            -876,649,763
            -618,-824,-621
            553,345,-567
            474,580,667
            -447,-329,318
            -584,868,-557
            544,-627,-890
            564,392,-477
            455,729,728
            -892,524,684
            -689,845,-530
            423,-701,434
            7,-33,-71
            630,319,-379
            443,580,662
            -789,900,-551
            459,-707,401

            --- scanner 1 ---
            686,422,578
            605,423,415
            515,917,-361
            -336,658,858
            95,138,22
            -476,619,847
            -340,-569,-846
            567,-361,727
            -460,603,-452
            669,-402,600
            729,430,532
            -500,-761,534
            -322,571,750
            -466,-666,-811
            -429,-592,574
            -355,545,-477
            703,-491,-529
            -328,-685,520
            413,935,-424
            -391,539,-444
            586,-435,557
            -364,-763,-893
            807,-499,-711
            755,-354,-619
            553,889,-390

            --- scanner 2 ---
            649,640,665
            682,-795,504
            -784,533,-524
            -644,584,-595
            -588,-843,648
            -30,6,44
            -674,560,763
            500,723,-460
            609,671,-379
            -555,-800,653
            -675,-892,-343
            697,-426,-610
            578,704,681
            493,664,-388
            -671,-858,530
            -667,343,800
            571,-461,-707
            -138,-166,112
            -889,563,-600
            646,-828,498
            640,759,510
            -630,509,768
            -681,-892,-333
            673,-379,-804
            -742,-814,-386
            577,-820,562

            --- scanner 3 ---
            -589,542,597
            605,-692,669
            -500,565,-823
            -660,373,557
            -458,-679,-417
            -488,449,543
            -626,468,-788
            338,-750,-386
            528,-832,-391
            562,-778,733
            -938,-730,414
            543,643,-506
            -524,371,-870
            407,773,750
            -104,29,83
            378,-903,-323
            -778,-728,485
            426,699,580
            -438,-605,-362
            -469,-447,-387
            509,732,623
            647,635,-688
            -868,-804,481
            614,-800,639
            595,780,-596

            --- scanner 4 ---
            727,592,562
            -293,-554,779
            441,611,-461
            -714,465,-776
            -743,427,-804
            -660,-479,-426
            832,-632,460
            927,-485,-438
            408,393,-506
            466,436,-512
            110,16,151
            -258,-428,682
            -393,719,612
            -211,-452,876
            808,-476,-593
            -575,615,604
            -485,667,467
            -680,325,-822
            -627,-443,-432
            872,-547,-609
            833,512,582
            807,604,487
            839,-516,451
            891,-625,532
            -652,-548,-490
            30,-46,-14         
            ";

        BeaconScanner beaconScanner = new BeaconScanner();
        List<Scanner> scanners = beaconScanner.GetInput(input);

        beaconScanner.CalculateScannerCoordinates(scanners);

        Assert.AreEqual(new Coordinates(0, 0, 0), scanners[0].Position);
        Assert.AreEqual(new Coordinates(68, -1246, -43), scanners[1].Position);
        Assert.AreEqual(new Coordinates(1105, -1205, 1229), scanners[2].Position);
        Assert.AreEqual(new Coordinates(-92, -2380, -20), scanners[3].Position);
        Assert.AreEqual(new Coordinates(-20, -1133, 1061), scanners[4].Position);
    }


    [Test]
    public void GetRotations_should_get_rotations()
    {
        string input = @"
            --- scanner 0 ---
            -1,-1,1
            -2,-2,2
            -3,-3,3
            -2,-3,1
            5,6,-4
            8,0,7
            ";

        BeaconScanner beaconScanner = new BeaconScanner();
        List<Scanner> scanners = beaconScanner.GetInput(input);
        Scanner scanner = scanners[0];

        List<List<Coordinates>> rotations = scanner.GetRotations();

        Assert.AreEqual(24, rotations.Count);

        List<Coordinates> rotation1 = new List<Coordinates>
        {
            new Coordinates(1,-1,1),
            new Coordinates(2,-2,2),
            new Coordinates(3,-3,3),
            new Coordinates(2,-1,3),
            new Coordinates(-5,4,-6),
            new Coordinates(-8,-7,0)
        };

        List<Coordinates> rotation2 = new List<Coordinates>
        {
            new Coordinates(-1,-1,-1),
            new Coordinates(-2,-2,-2),
            new Coordinates(-3,-3,-3),
            new Coordinates(-1,-3,-2),
            new Coordinates(4,6,5),
            new Coordinates(-7,0,8)
        };

        bool containsRotation2 = rotations.Any(rr => rr.Count == rotation2.Count && rr.All(r => rotation2.Contains(r)));
        Assert.IsTrue(containsRotation2);

        List<Coordinates> rotation3 = new List<Coordinates>
        {
            new Coordinates(1,1,-1),
            new Coordinates(2,2,-2),
            new Coordinates(3,3,-3),
            new Coordinates(1,3,-2),
            new Coordinates(-4,-6,5),
            new Coordinates(7,0,8)
        };

        bool containsRotation3 = rotations.Any(rr => rr.Count == rotation3.Count && rr.All(r => rotation3.Contains(r)));
        Assert.IsTrue(containsRotation3);

        List<Coordinates> rotation4 = new List<Coordinates>
        {
            new Coordinates(1,1,1),
            new Coordinates(2,2,2),
            new Coordinates(3,3,3),
            new Coordinates(3,1,2),
            new Coordinates(-6,-4,-5),
            new Coordinates(0,7,-8)
        };

        bool containsRotation4 = rotations.Any(rr => rr.Count == rotation4.Count && rr.All(r => rotation4.Contains(r)));
        Assert.IsTrue(containsRotation4);
    }

    [Test]
    public void GetBeaconCount_should_return_correct_count()
    {
        string input = @"
            --- scanner 0 ---
            404,-588,-901
            528,-643,409
            -838,591,734
            390,-675,-793
            -537,-823,-458
            -485,-357,347
            -345,-311,381
            -661,-816,-575
            -876,649,763
            -618,-824,-621
            553,345,-567
            474,580,667
            -447,-329,318
            -584,868,-557
            544,-627,-890
            564,392,-477
            455,729,728
            -892,524,684
            -689,845,-530
            423,-701,434
            7,-33,-71
            630,319,-379
            443,580,662
            -789,900,-551
            459,-707,401

            --- scanner 1 ---
            686,422,578
            605,423,415
            515,917,-361
            -336,658,858
            95,138,22
            -476,619,847
            -340,-569,-846
            567,-361,727
            -460,603,-452
            669,-402,600
            729,430,532
            -500,-761,534
            -322,571,750
            -466,-666,-811
            -429,-592,574
            -355,545,-477
            703,-491,-529
            -328,-685,520
            413,935,-424
            -391,539,-444
            586,-435,557
            -364,-763,-893
            807,-499,-711
            755,-354,-619
            553,889,-390

            --- scanner 2 ---
            649,640,665
            682,-795,504
            -784,533,-524
            -644,584,-595
            -588,-843,648
            -30,6,44
            -674,560,763
            500,723,-460
            609,671,-379
            -555,-800,653
            -675,-892,-343
            697,-426,-610
            578,704,681
            493,664,-388
            -671,-858,530
            -667,343,800
            571,-461,-707
            -138,-166,112
            -889,563,-600
            646,-828,498
            640,759,510
            -630,509,768
            -681,-892,-333
            673,-379,-804
            -742,-814,-386
            577,-820,562

            --- scanner 3 ---
            -589,542,597
            605,-692,669
            -500,565,-823
            -660,373,557
            -458,-679,-417
            -488,449,543
            -626,468,-788
            338,-750,-386
            528,-832,-391
            562,-778,733
            -938,-730,414
            543,643,-506
            -524,371,-870
            407,773,750
            -104,29,83
            378,-903,-323
            -778,-728,485
            426,699,580
            -438,-605,-362
            -469,-447,-387
            509,732,623
            647,635,-688
            -868,-804,481
            614,-800,639
            595,780,-596

            --- scanner 4 ---
            727,592,562
            -293,-554,779
            441,611,-461
            -714,465,-776
            -743,427,-804
            -660,-479,-426
            832,-632,460
            927,-485,-438
            408,393,-506
            466,436,-512
            110,16,151
            -258,-428,682
            -393,719,612
            -211,-452,876
            808,-476,-593
            -575,615,604
            -485,667,467
            -680,325,-822
            -627,-443,-432
            872,-547,-609
            833,512,582
            807,604,487
            839,-516,451
            891,-625,532
            -652,-548,-490
            30,-46,-14
            ";

        BeaconScanner beaconScanner = new BeaconScanner();
        List<Scanner> scanners = beaconScanner.GetInput(input);

        beaconScanner.CalculateScannerCoordinates(scanners);
        long beaconCount = beaconScanner.GetBeaconCount(scanners);

        Assert.AreEqual(79, beaconCount);
    }

    [Test]
    public void GetBeaconCount_should_return_correct_count_2()
    {
        string input = @"
            --- scanner 0 ---
            404,-588,-901
            528,-643,409
            -838,591,734
            390,-675,-793
            -537,-823,-458
            -485,-357,347
            -345,-311,381
            -661,-816,-575
            -876,649,763
            -618,-824,-621
            553,345,-567
            474,580,667
            -447,-329,318
            -584,868,-557
            544,-627,-890
            564,392,-477
            455,729,728
            -892,524,684
            -689,845,-530
            423,-701,434
            7,-33,-71
            630,319,-379
            443,580,662
            -789,900,-551
            459,-707,401

            --- scanner 1 ---
            686,422,578
            605,423,415
            515,917,-361
            -336,658,858
            95,138,22
            -476,619,847
            -340,-569,-846
            567,-361,727
            -460,603,-452
            669,-402,600
            729,430,532
            -500,-761,534
            -322,571,750
            -466,-666,-811
            -429,-592,574
            -355,545,-477
            703,-491,-529
            -328,-685,520
            413,935,-424
            -391,539,-444
            586,-435,557
            -364,-763,-893
            807,-499,-711
            755,-354,-619
            553,889,-390
            ";

        BeaconScanner beaconScanner = new BeaconScanner();
        List<Scanner> scanners = beaconScanner.GetInput(input);

        beaconScanner.CalculateScannerCoordinates(scanners);
        long beaconCount = beaconScanner.GetBeaconCount(scanners);

        Assert.AreEqual(38, beaconCount);
    }
}