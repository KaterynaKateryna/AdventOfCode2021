using NUnit.Framework;

namespace AdventOfCode.Day20.Tests
{
    public class TrenchMapTests
    {
        [TestCase(1, 24)]
        [TestCase(2, 35)]
        public void GetLitPixelsAfterEnhancements_should_return_correct_number(int enhancements, long expected)
        {
            string inputRaw = @"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

                #..#.
                #....
                ##..#
                ..#..
                ..###";

            TrenchMap trenchMap = new TrenchMap();
            (bool[] algorithm, bool[][] image) = trenchMap.ParseInput(inputRaw);
            long result = trenchMap.GetLitPixelsAfterEnhancements(algorithm, image, enhancements);
            Assert.AreEqual(expected, result);
        }
    }
}