using System.Collections;

namespace AdventOfCode.Day3;

public class PowerConsumption
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
        int numberLength = input[0].Length;
        int[] zeroes = new int[numberLength];
        int[] ones = new int[numberLength];

        foreach (bool[] number in input)
        {
            for (int i = 0; i < numberLength; ++i)
            {
                if (number[i])
                {
                    ones[i] += 1;
                }
                else
                {
                    zeroes[i] += 1;
                }
            }
        }

        bool[] gammaBits = new bool[numberLength];
        for (int i = 0; i < numberLength; ++i)
        {
            gammaBits[i] = ones[i] > zeroes[i];
        }

        int gammaRateNumber = BoolArrayToInt(gammaBits);
        int epsilonRateNumber = BoolArrayToInt(gammaBits.Reverse().ToArray());

        return gammaRateNumber * epsilonRateNumber;
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
