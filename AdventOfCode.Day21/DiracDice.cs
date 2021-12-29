namespace AdventOfCode.Day21;

public class DiracDice
{
    public async Task<(int one, int two)> GetInput()
    {
        string[] input = await File.ReadAllLinesAsync("input.txt");
        int one = int.Parse(input[0].Split(" ").Last().Trim());
        int two = int.Parse(input[1].Split(" ").Last().Trim());
        return (one, two);
    }

    public long GetLosingScoreByNumberOfRolls(int one, int two)
    {
        long oneScore = 0;
        long twoScore = 0;
        int currentOnePosition = one;
        int currentTwoPosition = two;
        int numberOfRolls = 0;
        int currentRollValue = 0;
        bool firstPlayerTurn = true;

        while (oneScore < 1000 && twoScore < 1000)
        {
            int sumOfRolls = 0;
            for (int i = 0; i < 3; ++i)
            {
                currentRollValue = (currentRollValue + 1) % 100 == 0 ? 100 : (currentRollValue + 1) % 100;
                sumOfRolls += currentRollValue;
                numberOfRolls++;
            }

            if (firstPlayerTurn)
            {
                currentOnePosition = (currentOnePosition + sumOfRolls) % 10 == 0 ? 
                    10
                    : (currentOnePosition + sumOfRolls) % 10;
                oneScore += currentOnePosition;
                if (oneScore >= 1000)
                {
                    break;
                }
            }
            else
            {
                currentTwoPosition = (currentTwoPosition + sumOfRolls) % 10 == 0 ?
                    10
                    : (currentTwoPosition + sumOfRolls) % 10;
                twoScore += currentTwoPosition;
                if (twoScore >= 1000)
                {
                    break;
                }
            }
            firstPlayerTurn = !firstPlayerTurn;
        }

        return oneScore > twoScore ? numberOfRolls * twoScore : numberOfRolls * oneScore;
    }
}
