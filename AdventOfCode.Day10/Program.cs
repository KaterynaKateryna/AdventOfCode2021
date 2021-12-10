using AdventOfCode.Day10;

// day 10
SyntaxScoring syntaxScoring = new SyntaxScoring();
string[] input = await syntaxScoring.GetInput();

// part 1
long score = syntaxScoring.GetSyntaxErrorScore(input);
Console.WriteLine(score);
