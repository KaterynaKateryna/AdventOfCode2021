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

    [TestCase("C200B40A82", 3)]
    [TestCase("04005AC33890", 54)]
    [TestCase("880086C3E88112", 7)]
    [TestCase("CE00C43D881120", 9)]
    [TestCase("D8005AC2A8F0", 1)]
    [TestCase("F600BC2D8F", 0)]
    [TestCase("9C005AC2F8F0", 0)]
    [TestCase("9C0141080250320F1802104A08", 1)]
    [TestCase("D2FE28", 2021)]
    [TestCase("38006F45291200", 1)]
    [TestCase("EE00D40C823060", 3)]
    public void GetValueOfPacket_should_return_correct_value(string input, long expected)
    {
        PacketDecoder packetDecoder = new PacketDecoder();

        bool[] parsedInput = packetDecoder.ConvertHexToBits(input);
        long result = packetDecoder.GetValueOfPacket(parsedInput);

        result.Should().Be(expected);
    }

    [Test]
    public void GetValueOfPacket_should_return_correct_value_single_sum()
    {
        PacketDecoder packetDecoder = new PacketDecoder();

        OperatorPacket packet = new OperatorPacket();
        packet.TypeId = 0;
        packet.Packets.Add(new LiteralPacket { LiteralValue = 12 });

        long result = packetDecoder.GetValueOfPacket(packet);

        result.Should().Be(12);
    }

    [Test]
    public void GetValueOfPacket_should_return_correct_value_three_sum()
    {
        PacketDecoder packetDecoder = new PacketDecoder();

        OperatorPacket packet = new OperatorPacket();
        packet.TypeId = 0;
        packet.Packets.Add(new LiteralPacket { LiteralValue = 12 });
        packet.Packets.Add(new LiteralPacket { LiteralValue = 10 });
        packet.Packets.Add(new LiteralPacket { LiteralValue = 1 });

        long result = packetDecoder.GetValueOfPacket(packet);

        result.Should().Be(23);
    }

    [Test]
    public void GetValueOfPacket_should_return_correct_value_single_product()
    {
        PacketDecoder packetDecoder = new PacketDecoder();

        OperatorPacket packet = new OperatorPacket();
        packet.TypeId = 1;
        packet.Packets.Add(new LiteralPacket { LiteralValue = 12 });

        long result = packetDecoder.GetValueOfPacket(packet);

        result.Should().Be(12);
    }

    [Test]
    public void GetValueOfPacket_should_return_correct_value_three_product()
    {
        PacketDecoder packetDecoder = new PacketDecoder();

        OperatorPacket packet = new OperatorPacket();
        packet.TypeId = 1;
        packet.Packets.Add(new LiteralPacket { LiteralValue = 12 });
        packet.Packets.Add(new LiteralPacket { LiteralValue = 10 });
        packet.Packets.Add(new LiteralPacket { LiteralValue = 2 });

        long result = packetDecoder.GetValueOfPacket(packet);

        result.Should().Be(240);
    }
}
