using NUnit.Framework;
using System.Linq;

namespace AdventOfCode.Day18.Tests;
    
public class SnailfishTests
{
    [TestCase("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]")]
    [TestCase("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]")]
    [TestCase("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]")]
    [TestCase("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]")]
    [TestCase("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")]
    public void Explodes_should_return_true_and_transform_number_when_number_explodes(string input, string expeced)
    {
        // Arrange
        Snailfish snailfish = new Snailfish();
        SnailfishNumber number = SnailfishNumber.Parse(input);

        // Act
        bool result = snailfish.Explodes(number);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(expeced, number.ToString());
    }

    [Test]
    public void Add_should_add_and_reduce_correctly()
    {
        // Arrange
        string[] numbers = new string[]
        {
            "[[[[4,3],4],4],[7,[[8,4],9]]]",
            "[1,1]"
        };
        string expected = "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]";

        Snailfish snailfish = new Snailfish();
        var snailfishNumbers = snailfish.ParseSnailfishNumbers(numbers);

        // Act
        SnailfishNumber result = snailfish.Add(snailfishNumbers[0], snailfishNumbers[1]);

        // Assert
        Assert.AreEqual(expected, result.ToString());
    }

    [Test]
    public void Add_should_add_and_reduce_correctly_2()
    {
        // Arrange
        string[] numbers = new string[]
        {
            "[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]",
            "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]"
        };
        string expected = "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]";

        Snailfish snailfish = new Snailfish();
        var snailfishNumbers = snailfish.ParseSnailfishNumbers(numbers);

        // Act
        SnailfishNumber result = snailfish.Add(snailfishNumbers[0], snailfishNumbers[1]);

        // Assert
        Assert.AreEqual(expected, result.ToString());
    }

    [Test]
    public void Add_list_should_add_and_reduce_correctly()
    {
        // Arrange
        string[] numbers = new string[]
        {
            "[[[0,[4, 5]],[0, 0]],[[[4,5],[2,6]],[9,5]]]",
            "[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]",
            "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]",
            "[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]",
            "[7,[5,[[3,8],[1,4]]]]",
            "[[2,[2,2]],[8,[8,1]]]",
            "[2,9]",
            "[1,[[[9,3],9],[[9,0],[0,7]]]]",
            "[[[5,[7,4]],7],1]",
            "[[[[4,2],2],6],[8,7]]"
        };
        string expected = "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]";

        Snailfish snailfish = new Snailfish();
        var snailfishNumbers = snailfish.ParseSnailfishNumbers(numbers);

        // Act
        SnailfishNumber result = snailfish.Add(snailfishNumbers);

        // Assert
        Assert.AreEqual(expected, result.ToString());
    }
}