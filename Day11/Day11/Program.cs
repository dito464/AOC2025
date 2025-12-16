using Day11;

string[] lines = Input.INPUT.Split("\r\n");

Dictionary<string, List<string>> connections = new();
Dictionary<string, long> reverseConNumbers = new();
Dictionary<(string, bool, bool), long> reverseConNumbersWithObstacles = new();
foreach (string line in lines)
{
    string[] parts = line.Split(':');
    string name = parts[0];
    List<string> values = parts[1].Trim().Split(' ').ToList();
    connections[name] = values;
}

const string end = "out";
const string start = "you";
reverseConNumbers[end] = 1;
reverseConNumbersWithObstacles[(end, true, true)] = 1;
reverseConNumbersWithObstacles[(end, true, false)] = 0;
reverseConNumbersWithObstacles[(end, false, true)] = 0;
reverseConNumbersWithObstacles[(end, false, false)] = 0;
const string start2 = "svr";
const string dac = "dac";
const string fft = "fft";

long GetPathCount(string location)
{
    long result = 0;
    if (reverseConNumbers.TryGetValue(location, out result))
    {
        return result;
    }
    else
    {
        foreach (string nextLocation in connections[location])
        {
            result += GetPathCount(nextLocation);
        }
        reverseConNumbers.Add(location, result);
    }
    return result;
}

long GetPathCountPt2(string location, bool hitDac, bool hitFft)
{
    long result = 0;
    if (reverseConNumbersWithObstacles.TryGetValue((location, hitDac, hitFft), out result))
    {
        return result;
    }
    else
    {
        foreach (string nextLocation in connections[location])
        {
            // Generate values for all hitDac, hitFft combos but don't use;
            GetPathCountPt2(nextLocation, true, true);
            GetPathCountPt2(nextLocation, true, false);
            GetPathCountPt2(nextLocation, false, true);
            GetPathCountPt2(nextLocation, false, false);
            if (nextLocation == dac)
            {
                result += GetPathCountPt2(nextLocation, true, hitFft);
            }
            else if (nextLocation == fft)
            {
                result += GetPathCountPt2(nextLocation, hitDac, true);
            }
            else
            {
                result += GetPathCountPt2(nextLocation, hitDac, hitFft);
            }
        }
        reverseConNumbersWithObstacles.Add((location, hitDac, hitFft), result);
    }
    return result;
}

Console.WriteLine(GetPathCount(start));
Console.WriteLine(GetPathCountPt2(start2, false, false));
