using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day11.Tests;

public class DumboOctopusTests
{
    [TestCase(1, 9)]
    [TestCase(2, 9)]
    public void CountHighlightsAfterSteps_returns_correct_highlights(int steps, long expected)
    {
        Octopus[,] input = new Octopus[,]
        {
            { new Octopus(1), new Octopus(1), new Octopus(1), new Octopus(1), new Octopus(1) },
            { new Octopus(1), new Octopus(9), new Octopus(9), new Octopus(9), new Octopus(1) },
            { new Octopus(1), new Octopus(9), new Octopus(1), new Octopus(9), new Octopus(1) },
            { new Octopus(1), new Octopus(9), new Octopus(9), new Octopus(9), new Octopus(1) },
            { new Octopus(1), new Octopus(1), new Octopus(1), new Octopus(1), new Octopus(1) }
        };

        DumboOctopus dumboOctopus = new DumboOctopus();

        long count = dumboOctopus.CountHighlightsAfterSteps(input, steps);

        Assert.AreEqual(expected, count);
    }

    [Test]
    public void CountHighlightsAfterSteps_updates_octopuses_correctly()
    {
        Octopus[,] input = new Octopus[,]
        {
            { new Octopus(1), new Octopus(1), new Octopus(1), new Octopus(1), new Octopus(1) },
            { new Octopus(1), new Octopus(9), new Octopus(9), new Octopus(9), new Octopus(1) },
            { new Octopus(1), new Octopus(9), new Octopus(1), new Octopus(9), new Octopus(1) },
            { new Octopus(1), new Octopus(9), new Octopus(9), new Octopus(9), new Octopus(1) },
            { new Octopus(1), new Octopus(1), new Octopus(1), new Octopus(1), new Octopus(1) }
        };

        DumboOctopus dumboOctopus = new DumboOctopus();

        long count = dumboOctopus.CountHighlightsAfterSteps(input, 1);

        Octopus[,] expected = new Octopus[,]
        {
            { new Octopus(3), new Octopus(4), new Octopus(5), new Octopus(4), new Octopus(3) },
            { new Octopus(4), new Octopus(0) { IsHighlighted = true }, new Octopus(0) { IsHighlighted = true }, new Octopus(0) { IsHighlighted = true }, new Octopus(4) },
            { new Octopus(5), new Octopus(0) { IsHighlighted = true }, new Octopus(0) { IsHighlighted = true }, new Octopus(0) { IsHighlighted = true }, new Octopus(5) },
            { new Octopus(4), new Octopus(0) { IsHighlighted = true }, new Octopus(0) { IsHighlighted = true }, new Octopus(0) { IsHighlighted = true }, new Octopus(4) },
            { new Octopus(3), new Octopus(4), new Octopus(5), new Octopus(4), new Octopus(3) }
        };

        input.Should().BeEquivalentTo(expected);
    }

    [TestCase(1, 0)]
    [TestCase(2, 35)]
    [TestCase(3, 80)] // 35 + 45
    [TestCase(4, 96)] // 80 + 16
    [TestCase(10, 204)]
    [TestCase(100, 1656)]
    public void CountHighlightsAfterSteps_returns_correct_highlights_larger_example(int steps, long expected)
    {
        Octopus[,] input = new Octopus[,]
        {
            { 
                new Octopus(5), new Octopus(4), new Octopus(8), new Octopus(3), new Octopus(1),
                new Octopus(4), new Octopus(3), new Octopus(2), new Octopus(2), new Octopus(3)
            },
            {
                new Octopus(2), new Octopus(7), new Octopus(4), new Octopus(5), new Octopus(8),
                new Octopus(5), new Octopus(4), new Octopus(7), new Octopus(1), new Octopus(1)
            },
            {
                new Octopus(5), new Octopus(2), new Octopus(6), new Octopus(4), new Octopus(5),
                new Octopus(5), new Octopus(6), new Octopus(1), new Octopus(7), new Octopus(3)
            },
            {
                new Octopus(6), new Octopus(1), new Octopus(4), new Octopus(1), new Octopus(3),
                new Octopus(3), new Octopus(6), new Octopus(1), new Octopus(4), new Octopus(6)
            },
            {
                new Octopus(6), new Octopus(3), new Octopus(5), new Octopus(7), new Octopus(3),
                new Octopus(8), new Octopus(5), new Octopus(4), new Octopus(7), new Octopus(8)
            },
            {
                new Octopus(4), new Octopus(1), new Octopus(6), new Octopus(7), new Octopus(5),
                new Octopus(2), new Octopus(4), new Octopus(6), new Octopus(4), new Octopus(5)
            },
            {
                new Octopus(2), new Octopus(1), new Octopus(7), new Octopus(6), new Octopus(8),
                new Octopus(4), new Octopus(1), new Octopus(7), new Octopus(2), new Octopus(1)
            },
            {
                new Octopus(6), new Octopus(8), new Octopus(8), new Octopus(2), new Octopus(8),
                new Octopus(8), new Octopus(1), new Octopus(1), new Octopus(3), new Octopus(4)
            },
            {
                new Octopus(4), new Octopus(8), new Octopus(4), new Octopus(6), new Octopus(8),
                new Octopus(4), new Octopus(8), new Octopus(5), new Octopus(5), new Octopus(4)
            },
            {
                new Octopus(5), new Octopus(2), new Octopus(8), new Octopus(3), new Octopus(7),
                new Octopus(5), new Octopus(1), new Octopus(5), new Octopus(2), new Octopus(6)
            }
        };

        DumboOctopus dumboOctopus = new DumboOctopus();

        long count = dumboOctopus.CountHighlightsAfterSteps(input, steps);

        Assert.AreEqual(expected, count);
    }
}