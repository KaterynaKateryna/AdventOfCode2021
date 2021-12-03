using NUnit.Framework;
using System.Collections;

namespace AdventOfCode.Day3.Tests;

public class PowerConsumptionTests
{
    [Test]
    public void Calculate_should_return_correct_number()
    {
        PowerConsumption powerConsumption = new PowerConsumption();

        bool[][] input = new bool[3][]
        {
            new bool[5]{ false, false, true, false, false },
            new bool[5]{ true, true, true, true, false },
            new bool[5]{ true, false, true, true, false }
        };

        // gamma:   true  false true true  false = 22
        // epsilon: false true  true false true  = 13
        long expected = 286; // 22 * 13

        long result = powerConsumption.CalculatePowerConsumption(input);

        Assert.AreEqual(expected, result);
    }
}
