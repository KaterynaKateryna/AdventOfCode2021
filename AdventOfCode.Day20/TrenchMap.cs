namespace AdventOfCode.Day20;

public class TrenchMap
{
    public async Task<(bool[] algorithm, bool[][] image)> GetInput()
    {
        string inputRaw = await File.ReadAllTextAsync("input.txt");
        return ParseInput(inputRaw);
    }

    public (bool[] algorithm, bool[][] image) ParseInput(string inputRaw)
    {
        string[] lines = inputRaw.Split('\n');
        bool[] algorithm = lines.First().Select(x => x == '#').ToArray();
        bool[][] image = lines.Skip(2)
            .Where(l => !string.IsNullOrEmpty(l))
            .Select(x => x.Trim().Select(y => y == '#').ToArray())
            .ToArray();

        return (algorithm, image);
    }

    public long GetLitPixelsAfterEnhancements(bool[] algorithm, bool[][] image, int enhancemants)
    {
        bool flip = false;
        if (algorithm[0] && !algorithm[511])
        {
            flip = true;
        }

        bool backgroundPixel = false;
        for (int i = 0; i < enhancemants; ++i)
        { 
            image = EnhanceImage(algorithm, image, backgroundPixel);
            if (flip)
            {
                backgroundPixel = !backgroundPixel;
            }
        }
        return image.SelectMany(x => x.Select(y => y)).Count(x => x == true);
    }

    public bool[][] EnhanceImage(bool[] algorithm, bool[][] image, bool backgroundPixel)
    {
        bool[][] result = new bool[image.Length + 2][];
        for (int i = 0; i < result.Length; i++)
        { 
            result[i] = new bool[image[0].Length + 2];
            for (int j = 0; j < result[i].Length; j++)
            {
                result[i][j] = GetOutputPixel(algorithm, image, i - 1, j - 1, backgroundPixel);
            }
        }
        return result;
    }

    private bool GetOutputPixel(bool[] algorithm, bool[][] image, int i, int j, bool backgroundPixel)
    { 
        bool[] binaryNumber = new bool[9];
        for (int ind = 0; ind < binaryNumber.Length; ++ind)
        {
            binaryNumber[ind] = backgroundPixel;
        }

        if (IsWithinBounds(i-1, j-1, image))
        {
            binaryNumber[0] = image[i - 1][j - 1];
        }
        if (IsWithinBounds(i - 1, j, image))
        {
            binaryNumber[1] = image[i - 1][j];
        }
        if (IsWithinBounds(i - 1, j + 1, image))
        {
            binaryNumber[2] = image[i - 1][j + 1];
        }
        if (IsWithinBounds(i, j - 1, image))
        {
            binaryNumber[3] = image[i][j - 1];
        }

        if (IsWithinBounds(i, j, image))
        {
            binaryNumber[4] = image[i][j];
        }

        if (IsWithinBounds(i, j + 1, image))
        {
            binaryNumber[5] = image[i][j + 1];
        }
        if (IsWithinBounds(i + 1, j - 1, image))
        {
            binaryNumber[6] = image[i + 1][j - 1];
        }
        if (IsWithinBounds(i + 1, j, image))
        {
            binaryNumber[7] = image[i + 1][j];
        }
        if (IsWithinBounds(i + 1, j + 1, image))
        {
            binaryNumber[8] = image[i + 1][j + 1];
        }

        long number = BoolArrayToInt(binaryNumber);

        return algorithm[number];
    }

    private bool IsWithinBounds(int i, int j, bool[][] image)
    {
        return i >= 0 && j >= 0 && i < image.Length && j < image[0].Length;
    }

    private static long BoolArrayToInt(bool[] bits)
    {
        if (bits.Length > 64)
        {
            throw new NotImplementedException();
        }

        long result = 0;
        for (int i = 0; i < bits.Length; i++)
        {
            if (bits[i])
            {
                result |= 1L << (bits.Length - i - 1);
            }
        }
        return result;
    }
}

