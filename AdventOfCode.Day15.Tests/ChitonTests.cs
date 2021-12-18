using NUnit.Framework;

namespace AdventOfCode.Day15.Tests;

public class ChitonTests
{
    [Test]
    public void Chiton_should_return_shortest_path()
    {
        int[][] input = new int[][]
        {
            new int[]{ 1, 1, 6, 3, 7, 5, 1, 7, 4, 2 },
            new int[]{ 1, 3, 8, 1, 3, 7, 3, 6, 7, 2 },
            new int[]{ 2, 1, 3, 6, 5, 1, 1, 3, 2, 8 },
            new int[]{ 3, 6, 9, 4, 9, 3, 1, 5, 6, 9 },
            new int[]{ 7, 4, 6, 3, 4, 1, 7, 1, 1, 1 },
            new int[]{ 1, 3, 1, 9, 1, 2, 8, 1, 3, 7 },
            new int[]{ 1, 3, 5, 9, 9, 1, 2, 4, 2, 1 },
            new int[]{ 3, 1, 2, 5, 4, 2, 1, 6, 3, 9 },
            new int[]{ 1, 2, 9, 3, 1, 3, 8, 5, 2, 1 },
            new int[]{ 2, 3, 1, 1, 9, 4, 4, 5, 8, 1 }
        };

        Chiton chiton = new Chiton();
        int result = chiton.GetShortestPathGreedy(input);

        Assert.AreEqual(40, result);
    }

    [Test]
    public void Chiton_should_return_shortest_path_2()
    {
        int[][] input = new int[][]
        {
            new int[]{ 1, 1, 6 },
            new int[]{ 1, 9, 8 },
            new int[]{ 2, 1, 3 }
        };

        Chiton chiton = new Chiton();
        int result = chiton.GetShortestPathGreedy(input);

        Assert.AreEqual(7, result);
    }

    [Test]
    public void Chiton_should_return_shortest_path_3()
    {
        int[][] input = new int[][]
        {
            new int[]{ 1, 3, 6, 3, 7 },
            new int[]{ 1, 1, 8, 9, 6 },
            new int[]{ 9, 1, 9, 9, 8 },
            new int[]{ 9, 1, 1, 1, 1 },
            new int[]{ 9, 1, 3, 6, 1 }
        };

        Chiton chiton = new Chiton();
        int result = chiton.GetShortestPathGreedy(input);

        Assert.AreEqual(8, result);
    }

    [Test]
    public void Chiton_should_build_full_map()
    {
        int[][] input = new int[][]
        {
            new int[]{ 1 }
        };

        Chiton chiton = new Chiton();
        int[][] result = chiton.GetFullMap(input);

        int[][] expected = new int[][]
        {
            new int[]{ 1, 2, 3, 4, 5 },
            new int[]{ 2, 3, 4, 5, 6 },
            new int[]{ 3, 4, 5, 6, 7 },
            new int[]{ 4, 5, 6, 7, 8 },
            new int[]{ 5, 6, 7, 8, 9 }
        };

        Assert.AreEqual(expected.Length, result.Length);

        for (int i = 0; i < result.Length; ++i)
        {
            Assert.AreEqual(expected[i].Length, result[i].Length);
            for (int j = 0; j < result[0].Length; ++j)
            {
                Assert.AreEqual(expected[i][j], result[i][j]);
            }
        }
    }

    [Test]
    public void Chiton_should_build_full_map_2()
    {
        int[][] input = new int[][]
        {
            new int[]{ 8 }
        };

        Chiton chiton = new Chiton();
        int[][] result = chiton.GetFullMap(input);

        int[][] expected = new int[][]
        {
            new int[]{ 8, 9, 1, 2, 3 },
            new int[]{ 9, 1, 2, 3, 4 },
            new int[]{ 1, 2, 3, 4, 5 },
            new int[]{ 2, 3, 4, 5, 6 },
            new int[]{ 3, 4, 5, 6, 7 }
        };

        Assert.AreEqual(expected.Length, result.Length);

        for (int i = 0; i < result.Length; ++i)
        {
            Assert.AreEqual(expected[i].Length, result[i].Length);
            for (int j = 0; j < result[0].Length; ++j)
            {
                Assert.AreEqual(expected[i][j], result[i][j]);
            }
        }
    }
}