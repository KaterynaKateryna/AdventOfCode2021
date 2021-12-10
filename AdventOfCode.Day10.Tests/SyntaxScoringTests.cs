using NUnit.Framework;

namespace AdventOfCode.Day10.Tests;

public class SyntaxScoringTests
{
    [Test]
    public void GetMiddleAutocompleteScore_should_return_correct_score()
    {
        string[] input = new string[] 
        {
            "[({(<(())[]>[[{[]{<()<>>",
            "[(()[<>])]({[<{<<[]>>(",
            "{([(<{}[<>[]}>{[]{[(<()>",
            "(((({<>}<{<{<>}{[]{[]{}",
            "[[<[([]))<([[{}[[()]]]",
            "[{[{({}]{}}([{[{{{}}([]",
            "{<[[]]>}<{[{[{[]{()[[[]",
            "[<(<(<(<{}))><([]([]()",
            "<{([([[(<>()){}]>(<<{{",
            "<{([{{}}[<[[[<>{}]]]>[]]"
        };
        SyntaxScoring syntaxScoring = new SyntaxScoring();

        long result = syntaxScoring.GetMiddleAutocompleteScore(input);

        Assert.AreEqual(288957, result);
    }
}