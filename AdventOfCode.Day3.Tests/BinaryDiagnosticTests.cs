using NUnit.Framework;
using System.Collections;

namespace AdventOfCode.Day3.Tests;

public class BinaryDiagnosticTests
{
    [Test]
    public void CalculatePowerConsumption_should_return_correct_number()
    {
        BinaryDiagnostic diagnostic = new BinaryDiagnostic();

        bool[][] input = new bool[3][]
        {
            new bool[5]{ false, false, true, false, false },
            new bool[5]{ true, true, true, true, false },
            new bool[5]{ true, false, true, true, false }
        };

        // gamma:   true  false true true  false = 22
        // epsilon: false true  true false true  = 13
        long expected = 286; // 22 * 13

        long result = diagnostic.CalculatePowerConsumption(input);

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void CalculateLifeSupportRating_should_return_correct_number()
    {
        BinaryDiagnostic diagnostic = new BinaryDiagnostic();

        bool[][] input = new bool[12][]
        {
            new bool[5]{ false, false, true, false, false },
            new bool[5]{ true, true, true, true, false },
            new bool[5]{ true, false, true, true, false },
            new bool[5]{ true, false, true, true, true },
            new bool[5]{ true, false, true, false, true },
            new bool[5]{ false, true, true, true, true },
            new bool[5]{ false, false, true, true, true },
            new bool[5]{ true, true, true, false, false },
            new bool[5]{ true, false, false, false, false },
            new bool[5]{ true, true, false, false, true },
            new bool[5]{ false, false, false, true, false },
            new bool[5]{ false, true, false, true, false }
        };

        long expected = 230;

        long result = diagnostic.CalculateLifeSupportRating(input);

        Assert.AreEqual(expected, result);
    }
}
