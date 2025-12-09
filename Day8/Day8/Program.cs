using System.Data;
using System.Security.Cryptography;
using Day8;

string[] lines = Input.INPUT.Split("\r\n");

SortedDictionary<double, (Coordinate, Coordinate)> sizes = new();
Dictionary<Coordinate, HashSet<Coordinate>> connections = new();

Coordinate[] coordinates = new Coordinate[lines.Length];
HashSet<Coordinate> seenAdded = new();

for (int i = 0; i < lines.Length; i++)
{
    string[] coords = lines[i].Split(',');
    Coordinate coordinate = new Coordinate(
        int.Parse(coords[0]),
        int.Parse(coords[1]),
        int.Parse(coords[2])
    );
    coordinates[i] = coordinate;
}

var straightLineDistance = (Coordinate c1, Coordinate c2) =>
{
    long x = c1.x - c2.x;
    long y = c1.y - c2.y;
    long z = c1.z - c2.z;
    return Math.Sqrt(x * x + y * y + z * z);
};

for (int i = 0; i < coordinates.Length - 1; i++)
{
    for (int j = i + 1; j < coordinates.Length; j++)
    {
        sizes.Add(
            straightLineDistance(coordinates[i], coordinates[j]),
            (coordinates[i], coordinates[j])
        );
    }
}

int connectionCount = 0;
const int maxConnections = 1000;

foreach (var kvp in sizes)
{
    (Coordinate c1, Coordinate c2) = kvp.Value;
    HashSet<Coordinate> possibleC1s = null;
    HashSet<Coordinate> possibleC2s = null;
    connections.TryGetValue(c1, out possibleC1s);
    connections.TryGetValue(c2, out possibleC2s);

    if (possibleC1s == null && possibleC2s == null)
    {
        HashSet<Coordinate> set = new HashSet<Coordinate>() { c1, c2 };
        connections.Add(c1, set);
        connections.Add(c2, set);
    }
    else if (possibleC1s == null && possibleC2s != null)
    {
        possibleC2s.Add(c1);
        connections.Add(c1, possibleC2s);
    }
    else if (possibleC1s != null && possibleC2s == null)
    {
        possibleC1s.Add(c2);
        connections.Add(c2, possibleC1s);
    }
    else if (!possibleC1s.Contains(c2) && !possibleC2s.Contains(c1))
    {
        foreach (var connection in possibleC1s)
        {
            possibleC2s.Add(connection);
        }
        foreach (var connection in possibleC1s)
        {
            connections[connection] = possibleC2s;
        }
    }
    /*else
    {
        continue;
    }*/

    connectionCount++;
    if (connectionCount == maxConnections)
    {
        int result = 1;
        int groupCount = 0;
        const int largestGroups = 3;
        HashSet<Coordinate> seenRemoved = new();
        while (connections.Count > 0)
        {
            var kvp2 = connections.MaxBy(x => x.Value.Count);
            result *= kvp2.Value.Count;
            foreach (var connection in kvp2.Value)
            {
                if (seenRemoved.Contains(connection))
                {
                    Console.WriteLine(
                        $"Duplicate Coord {connection.x},{connection.y},{connection.z}"
                    );
                }
                else
                {
                    seenRemoved.Add(connection);
                }
                connections.Remove(connection);
            }
            groupCount++;
            if (groupCount == largestGroups)
            {
                break;
            }
        }

        Console.WriteLine(result);
    }
    if (possibleC2s != null && possibleC2s.Count == lines.Length)
    {
        Console.WriteLine((long)c1.x * (long)c2.x);
        break;
    }
}
