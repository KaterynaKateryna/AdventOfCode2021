using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Day16.Tests;

public class PacketDecoderTests
{
    [Test]
    public void ConvertHexToBits_should_convert_correctly()
    {
        string input = "D2";
        bool[] expected = new bool[]
        {
            true, true, false, true,
            false, false, true, false
        };
        PacketDecoder packetDecoder = new PacketDecoder();

        bool[] result = packetDecoder.ConvertHexToBits(input);

        result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Test]
    public void ConvertHexToBits_should_convert_correctly_2()
    {
        string input = "D2FE28";
        bool[] expected = new bool[] 
        { 
            true, true, false, true,
            false, false, true, false,
            true, true, true, true,
            true, true, true, false,
            false, false, true, false,
            true, false, false, false
        };
        PacketDecoder packetDecoder = new PacketDecoder();
        
        bool[] result = packetDecoder.ConvertHexToBits(input);

        result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [TestCase("8A004A801A8002F478", 16)]
    [TestCase("620080001611562C8802118E34", 12)]
    [TestCase("C0015000016115A2E0802F182340", 23)]
    public void VersionNumbersSum_should_return_correct_sum(string input, int expected)
    {
        PacketDecoder packetDecoder = new PacketDecoder();

        bool[] parsedInput = packetDecoder.ConvertHexToBits(input);
        int result = packetDecoder.VersionNumbersSum(parsedInput);

        result.Should().Be(expected);
    }
}
