namespace AdventOfCode.Day16;

public class PacketDecoder
{
    public Task<string> GetInput()
    {
        return File.ReadAllTextAsync("input.txt");
    }

    public bool[] ConvertHexToBits(string input)
    {
        bool[] result = new bool[input.Length * 4];
        for (int i = 0; i < input.Length; ++i)
        {
            if (input[i] == '\n')
            {
                continue;
            }

            ushort number = UInt16.Parse(input[i].ToString(), System.Globalization.NumberStyles.HexNumber);
            byte[] bytes = BitConverter.GetBytes(number);
            string bitsString = Convert.ToString(bytes[0], 2);
            while (bitsString.Length < 4)
            {
                bitsString = new string(bitsString.Prepend('0').ToArray());
            }

            bool[] bits = new bool[4];
            for (int j = 0; j < 4; ++j)
            {
                bits[j] = bitsString[j] == '1';
            }

            Array.Copy(bits, 0, result, i*4, 4);
        }
        return result;
    }

    public int VersionNumbersSum(bool[] input)
    {
        Packet rootPacket = PacketFactory.Parse(input);
        return VersionNumbersSum(rootPacket);
    }

    private int VersionNumbersSum(Packet packet)
    {
        int versionsSum = 0;
        versionsSum += packet.Version;

        if (packet is OperatorPacket)
        {
            OperatorPacket operatorPacket = (OperatorPacket)packet;
            foreach (Packet subPacket in operatorPacket.Packets)
            { 
                versionsSum += VersionNumbersSum(subPacket);
            }
        }

        return versionsSum;
    }


    public long GetValueOfPacket(bool[] input)
    {
        Packet rootPacket = PacketFactory.Parse(input);
        return GetValueOfPacket(rootPacket);
    }

    public long GetValueOfPacket(Packet packet)
    {
        if (packet is LiteralPacket)
        {
            return ((LiteralPacket)packet).LiteralValue;
        }

        OperatorPacket operatorPacket = (OperatorPacket)packet;
        List<long> values = operatorPacket.Packets.Select(p => GetValueOfPacket(p)).ToList();
        switch (operatorPacket.TypeId)
        {
            case 0:
                return checked(values.Sum());
            case 1:
                return checked(values.Aggregate((a, b) => a * b));
            case 2:
                return values.Min();
            case 3:
                return values.Max();
            case 5:
                return values[0] > values[1] ? 1 : 0;
            case 6:
                return values[0] < values[1] ? 1 : 0;
            case 7:
                return values[0] == values[1] ? 1 : 0;
            default:
                throw new ArgumentException("Unsupported operator");
        }
    }
}

public class PacketFactory
{
    public static Packet Parse(bool[] input)
    {
        int version = (int)BoolArrayToInt(input.Take(3).ToArray());
        int typeId = (int)BoolArrayToInt(input.Skip(3).Take(3).ToArray());

        if (typeId == 4)
        {
            (long literal, int literalLength) = GetLiteralValue(input.Skip(6).ToArray());

            return new LiteralPacket 
            { 
                Version = version, 
                TypeId = typeId, 
                LiteralValue = literal, 
                PacketLength = 6 + literalLength
            };
        }
        else
        {
            OperatorPacket packet = new OperatorPacket { Version = version, TypeId = typeId };
            packet.LengthTypeId = input.Skip(6).First() ? 1 : 0;

            if (packet.LengthTypeId == 1)
            {
                int numberOfSubPackets = (int)BoolArrayToInt(input.Skip(7).Take(11).ToArray());
                int parsedSubpacketsNumber = 0;
                int parsedSubpacketsLength = 0;
                while (parsedSubpacketsNumber < numberOfSubPackets)
                {
                    Packet subPacket = PacketFactory.Parse(input.Skip(18 + parsedSubpacketsLength).ToArray());
                    parsedSubpacketsLength += subPacket.PacketLength;
                    parsedSubpacketsNumber++;
                    packet.Packets.Add(subPacket);
                }
                packet.PacketLength = 18 + parsedSubpacketsLength;
                return packet;
            }
            else
            {
                int totalLengthOfSubPackets = (int)BoolArrayToInt(input.Skip(7).Take(15).ToArray());
                int parsedSubpacketsLength = 0;

                while (parsedSubpacketsLength < totalLengthOfSubPackets)
                {
                    Packet subPacket = PacketFactory.Parse(input.Skip(22 + parsedSubpacketsLength).ToArray());
                    parsedSubpacketsLength += subPacket.PacketLength;
                    packet.Packets.Add(subPacket);
                }
                packet.PacketLength = 22 + parsedSubpacketsLength;
                return packet;
            }
        }
    }

    private static (long literal, int length) GetLiteralValue(bool[] input)
    {
        int position = 0;
        bool leadingBit = input[position];
        List<bool> literalBits = new List<bool>();
        while (true)
        {
            literalBits.AddRange(input.Skip(position + 1).Take(4));
            position += 5;
            if (!leadingBit)
            {
                break;
            }
            leadingBit = input[position];
        }
        return (BoolArrayToInt(literalBits.ToArray()), position);
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

public class Packet
{
    public int Version { get; set; }

    public int TypeId { get; set; }

    public int PacketLength { get; set; }
}

public class LiteralPacket : Packet
{ 
    public long LiteralValue { get; set; }
}

public class OperatorPacket : Packet
{
    public OperatorPacket()
    {
        Packets = new List<Packet>();
    }

    public int LengthTypeId { get; set; }

    public List<Packet> Packets { get; set; }
}
