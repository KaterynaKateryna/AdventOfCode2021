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

    public long GetWinningUniverses(int one, int two)
    {
        (long oneWins, long twoWins) = GetWinningUniverses(one, two, 0, 0, 0, true);
        return oneWins > twoWins ? oneWins : twoWins;
    }

    private (long oneWins, long twoWins) GetWinningUniverses(
        int currentOnePosition,
        int currentTwoPosition,
        long oneScore,
        long twoScore,
        int sumOfRolls,
        bool firstPlayerTurn
    )
    {
        if (sumOfRolls != 0)
        {
            if (firstPlayerTurn)
            {
                currentOnePosition = (currentOnePosition + sumOfRolls) % 10 == 0 ?
                    10
                    : (currentOnePosition + sumOfRolls) % 10;
                oneScore += currentOnePosition;
            }
            else
            {
                currentTwoPosition = (currentTwoPosition + sumOfRolls) % 10 == 0 ?
                    10
                    : (currentTwoPosition + sumOfRolls) % 10;
                twoScore += currentTwoPosition;
            }
            firstPlayerTurn = !firstPlayerTurn;
        }

        if (oneScore >= 21)
        {
            return (1, 0);
        }
        if (twoScore >= 21)
        {
            return (0, 1);
        }

        (long winsOne1, long winsTwo1) = GetWinningUniverses(
            currentOnePosition, currentTwoPosition, oneScore, twoScore, 3, firstPlayerTurn);
        (long winsOne2, long winsTwo2) = GetWinningUniverses(
            currentOnePosition, currentTwoPosition, oneScore, twoScore, 4, firstPlayerTurn);
        (long winsOne3, long winsTwo3) = GetWinningUniverses(
            currentOnePosition, currentTwoPosition, oneScore, twoScore, 5, firstPlayerTurn);
        (long winsOne4, long winsTwo4) = GetWinningUniverses(
            currentOnePosition, currentTwoPosition, oneScore, twoScore, 6, firstPlayerTurn);
        (long winsOne5, long winsTwo5) = GetWinningUniverses(
            currentOnePosition, currentTwoPosition, oneScore, twoScore, 7, firstPlayerTurn);
        (long winsOne6, long winsTwo6) = GetWinningUniverses(
            currentOnePosition, currentTwoPosition, oneScore, twoScore, 8, firstPlayerTurn);
        (long winsOne7, long winsTwo7) = GetWinningUniverses(
            currentOnePosition, currentTwoPosition, oneScore, twoScore, 9, firstPlayerTurn);

        long wins1 = winsOne1 + (3 * winsOne2) + (6 * winsOne3) + (7 * winsOne4) + (6 * winsOne5) + (3 * winsOne6) + winsOne7;
        long wins2 = winsTwo1 + (3 * winsTwo2) + (6 * winsTwo3) + (7 * winsTwo4) + (6 * winsTwo5) + (3 * winsTwo6) + winsTwo7;

        return (wins1, wins2);
    }
}
