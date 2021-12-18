using AdventOfCode.Day15;

// day 15
Chiton chiton = new Chiton();
int[][] input = await chiton.GetInput();

// part 1
int result = chiton.GetShortestPathGreedy(input);
Console.WriteLine(result);

// part 2 
int[][] fullMap = chiton.GetFullMap(input);
int resultFull = chiton.GetShortestPathGreedy(fullMap);
Console.WriteLine(resultFull);