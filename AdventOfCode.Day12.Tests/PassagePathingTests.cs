using NUnit.Framework;

namespace AdventOfCode.Day12.Tests;

public class PassagePathingTests
{

    [Test]
    public void GetNumberOfPossiblePaths_should_return_correct_number()
    {
        string[] input = new string[]
        {
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end"
        };

        PassagePathing passagePathing = new PassagePathing();

        int result = passagePathing.GetNumberOfPossiblePaths(input);

        Assert.AreEqual(10, result);
    }

    [Test]
    public void GetNumberOfPossiblePaths_should_return_correct_number_2()
    {
        string[] input = new string[]
        {
            "dc-end",
            "HN-start",
            "start-kj",
            "dc-start",
            "dc-HN",
            "LN-dc",
            "HN-end",
            "kj-sa",
            "kj-HN",
            "kj-dc"
        };

        PassagePathing passagePathing = new PassagePathing();

        int result = passagePathing.GetNumberOfPossiblePaths(input);

        Assert.AreEqual(19, result);
    }

    [Test]
    public void GetNumberOfPossiblePaths_should_return_correct_number_3()
    {
        string[] input = new string[]
        {
            "fs-end",
            "he-DX",
            "fs-he",
            "start-DX",
            "pj-DX",
            "end-zg",
            "zg-sl",
            "zg-pj",
            "pj-he",
            "RW-he",
            "fs-DX",
            "pj-RW",
            "zg-RW",
            "start-pj",
            "he-WI",
            "zg-he",
            "pj-fs",
            "start-RW"
        };

        PassagePathing passagePathing = new PassagePathing();

        int result = passagePathing.GetNumberOfPossiblePaths(input);

        Assert.AreEqual(226, result);
    }
}
