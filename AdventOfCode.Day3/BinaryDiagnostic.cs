using System.Collections;

namespace AdventOfCode.Day3;

public class BinaryDiagnostic
{
    public async Task<bool[][]> GetInput()
    {
        string[] inputRaw = await File.ReadAllLinesAsync("input.txt");
        return inputRaw.Select(x => ParseBoolArray(x)).ToArray();

        bool[] ParseBoolArray(string input)
        {
            return input.Select(x => x == '1').ToArray();
        }
    }

    public long CalculatePowerConsumption(bool[][] input)
    {
        var gammaBits = GetGammaEpsilonBits(input, input[0].Length, 0, (a, b) => a >= b);

        int gammaRateNumber = BoolArrayToInt(gammaBits);
        int epsilonRateNumber = BoolArrayToInt(gammaBits.Reverse().ToArray());

        return gammaRateNumber * epsilonRateNumber;
    }

    public long CalculateLifeSupportRating(bool[][] input)
    {
        long oxygenGeneratorRating = CalculateRating(input, 0, (a, b) => a >= b);
        long co2ScrubberRating = CalculateRating(input, 0, (a, b) => a < b);

        return oxygenGeneratorRating * co2ScrubberRating;
    }

    private long CalculateRating(bool[][] input, int index, Func<int, int, bool> comparer)
    {
        if (input.Length == 1)
        {
            return BoolArrayToInt(input[0]);
        }    

        bool bitCriteria = GetGammaEpsilonBits(input, 1, index, comparer)[0];
        bool[][] newInput = input.Where(x => x[index] == bitCriteria).ToArray();

        return CalculateRating(newInput, index+1, comparer);
    }

    private bool[] GetGammaEpsilonBits(bool[][] input, int numberLength, int startIndex, Func<int, int, bool> comparer)
    {
        int[] zeroes = new int[numberLength];
        int[] ones = new int[numberLength];

        foreach (bool[] number in input)
        {
            for (int i = 0; i < numberLength; ++i)
            {
                if (number[startIndex + i])
                {
                    ones[i] += 1;
                }
                else
                {
                    zeroes[i] += 1;
                }
            }
        }

        bool[] resultBits = new bool[numberLength];
        for (int i = 0; i < numberLength; ++i)
        {
            resultBits[i] = comparer(ones[i], zeroes[i]);
        }
        return resultBits;
    }

    int BoolArrayToInt(bool[] bits)
    {
        int result = 0;
        for (int i = 0; i < bits.Length; i++)
        {
            if (bits[i])
            {
                result |= 1 << (bits.Length - i - 1);
            }
        }
        return result;
    }
}
