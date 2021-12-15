using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day13.Tests;

public class TransparentOrigamiTests
{
    [Test]
    public void TransparentOrigami_should_fold_correctly()
    {
        List<Point> points = new List<Point>()
        { 
            new Point(6, 10),
            new Point(0, 14),
            new Point(9, 10),
            new Point(0, 3),
            new Point(10, 4),
            new Point(4, 11),
            new Point(6, 0),
            new Point(6, 12),
            new Point(4, 1),
            new Point(0, 13),
            new Point(10, 12),
            new Point(3, 4),
            new Point(3, 0),
            new Point(8, 4),
            new Point(1, 10),
            new Point(2, 14),
            new Point(8, 10),
            new Point(9, 0)
        };

        TransparentOrigami transparentOrigami = new TransparentOrigami();

        // first fold
        Fold fold = new Fold(FoldDirection.Y, 7);
        HashSet<Point> result = transparentOrigami.Fold(points, fold);

        Assert.AreEqual(17, result.Count);

        // second fold
        Fold fold2 = new Fold(FoldDirection.X, 5);
        HashSet<Point> result2 = transparentOrigami.Fold(result, fold2);

        Assert.AreEqual(16, result2.Count);
    }
}