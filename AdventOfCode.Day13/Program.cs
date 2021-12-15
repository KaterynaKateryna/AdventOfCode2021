using AdventOfCode.Day13;

// day 13
TransparentOrigami transparentOrigami = new TransparentOrigami();
(List<Point> points, List<Fold> folds) = await transparentOrigami.GetInput();

// part 1
HashSet<Point> result = transparentOrigami.Fold(points, folds.First());
Console.WriteLine(result.Count());
