using NUnit.Framework;

namespace AdventOfCode.Day9.Tests;

public class SmokeBasinTests
{
    [Test]
    public void SumOfRiskLevels_should_return_correct_sum()
    {
        int[][] input = new int[5][]
        {
            new int[10] { 2, 1, 9, 9, 9, 4, 3, 2, 1, 0 },
            new int[10] { 3, 9, 8, 7, 8, 9, 4, 9, 2, 1 },
            new int[10] { 9, 8, 5, 6, 7, 8, 9, 8, 9, 2 },
            new int[10] { 8, 7, 6, 7, 8, 9, 6, 7, 8, 9 },
            new int[10] { 9, 8, 9, 9, 9, 6, 5, 6, 7, 8 }
        };
        SmokeBasin smokeBasin = new SmokeBasin();

        int sum = smokeBasin.SumOfRiskLevels(input);

        Assert.AreEqual(15, sum);
    }
}