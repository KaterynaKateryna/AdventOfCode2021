namespace AdventOfCode.Day11;

public class DumboOctopus
{
    public async Task<Octopus[,]> GetInput()
    {
        string[] lines = await File.ReadAllLinesAsync("input.txt");
        Octopus[,] input = new Octopus[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; ++j)
            {
                input[i, j] = new Octopus(int.Parse(lines[i][j].ToString()));
            }
        }
        return input;
    }

    public long CountHighlightsAfterSteps(Octopus[,] input, int steps)
    {
        long result = 0;
        for (int i = 0; i < steps; ++i)
        {
            PerformStep(input);
            result += CountHighlights(input);
        }
        return result;
    }

    public long FirstStepOctopusesSynchronize(Octopus[,] input)
    {
        long step = 0;
        long flashes = 0;
        while (flashes < input.Length)
        {
            PerformStep(input);
            step++;
            flashes = CountHighlights(input);
        }
        return step;
    }

    private void PerformStep(Octopus[,] input)
    {
        foreach (Octopus octopus in input)
        {
            octopus.IsHighlighted = false;
        }

        for (int i = 0; i < input.GetLength(0); i++)
        {
            for (int j = 0; j < input.GetLength(1); ++j)
            {
                IncrementAndFlash(input, i, j);
            }
        }
    }

    private long CountHighlights(Octopus[,] input)
    {
        long result = 0;
        foreach (Octopus octopus in input)
        { 
            result += octopus.IsHighlighted ? 1 : 0;
        }
        return result;
    }

    private void IncrementAndFlash(Octopus[,] input, int i, int j)
    {
        Octopus octopus = input[i, j];
        if (octopus.IsHighlighted == true)
        {
            return;
        }

        input[i, j].Value++;

        if (octopus.Value <= 9)
        {
            return;
        }

        octopus.IsHighlighted = true;
        octopus.Value = 0;

        if (i > 0)
        {
            if (j > 0)
            {
                IncrementAndFlash(input, i - 1, j - 1);
            }
            IncrementAndFlash(input, i - 1, j);
            if (j < input.GetLength(1) - 1)
            {
                IncrementAndFlash(input, i - 1, j + 1);
            }
        }

        if (j > 0)
        {
            IncrementAndFlash(input, i, j - 1);
        }
        if (j < input.GetLength(1) - 1)
        {
            IncrementAndFlash(input, i, j + 1);
        }

        if (i < input.GetLength(0) - 1)
        {
            if (j > 0)
            {
                IncrementAndFlash(input, i + 1, j - 1);
            }
            IncrementAndFlash(input, i + 1, j);
            if (j < input.GetLength(1) - 1)
            {
                IncrementAndFlash(input, i + 1, j + 1);
            }
        }
    }
}

public class Octopus
{
    public Octopus(int value)
    { 
        Value = value;
    }

    public int Value { get; set; }

    public bool IsHighlighted { get; set; }
}
