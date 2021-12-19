using AdventOfCode.Day16;

// day 16
PacketDecoder packetDecoder = new PacketDecoder();
string input = await packetDecoder.GetInput();
bool[] bits = packetDecoder.ConvertHexToBits(input);

// part 1
int res = packetDecoder.VersionNumbersSum(bits);
Console.WriteLine(res);

