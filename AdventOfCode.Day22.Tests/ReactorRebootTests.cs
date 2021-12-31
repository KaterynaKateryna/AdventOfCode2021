using NUnit.Framework;

namespace AdventOfCode.Day22.Tests;

public class ReactorRebootTests
{
    [Test]
    public void GetOnCubesOptimized_should_return_correct_count()
    {
        Command[] commands = new Command[]
        {
            new Command(true, 10, 12, 10, 12, 10, 12),
            new Command(true, 11, 13, 11, 13, 11, 13),
            new Command(false, 9, 11, 9, 11, 9, 11),
            new Command(true, 10, 10, 10, 10, 10, 10),
        };

        ReactorReboot reactorReboot = new ReactorReboot();

        long result = reactorReboot.GetOnCubesRecursive(commands, int.MinValue, int.MaxValue);

        Assert.AreEqual(39, result);
    }
}