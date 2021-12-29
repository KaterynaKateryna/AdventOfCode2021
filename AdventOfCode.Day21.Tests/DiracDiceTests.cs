using NUnit.Framework;

namespace AdventOfCode.Day21.Tests;

public class DiracDiceTests
{
    [Test]
    public void GetLosingScoreByNumberOfRolls_should_return_correct_value()
    {
        DiracDice dice = new DiracDice();
        long result = dice.GetLosingScoreByNumberOfRolls(4, 8);

        Assert.AreEqual(739785, result);
    }
}
