using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using Day9;

string[] lines = Input.INPUT.Split("\r\n");
SortedDictionary<long, List<long>> xToY = new();
SortedDictionary<long, List<long>> yToX = new();
(long x, long y)[] corners = new (long, long)[lines.Length];
Dictionary<(long, long), long> cornerMap = new();
SortedDictionary<long, List<long>> xToYEdges = new();
SortedDictionary<long, List<long>> yToXEdges = new();

long maxAreaPt1 = 0;
long maxAreaPt2 = 0;
var wrappedNext = (long val) =>
{
    return val == lines.Length - 1 ? 0 : val + 1;
};
var wrappedPrev = (long val) =>
{
    return val == 0 ? lines.Length - 1 : val - 1;
};

for (int i = 0; i < lines.Length; i++)
{
    string[] nums = lines[i].Split(',');
    long x = long.Parse(nums[0]);
    long y = long.Parse(nums[1]);
    if (!xToY.ContainsKey(x))
    {
        xToY[x] = new() { y };
    }
    else
    {
        xToY[x].Add(y);
    }
    if (!yToX.ContainsKey(y))
    {
        yToX[y] = new() { x };
    }
    else
    {
        yToX[y].Add(x);
    }
    corners[i] = (x, y);
    cornerMap[(x, y)] = i;
}

for (int i = 0; i < lines.Length; i++)
{
    var prevCorner = corners[wrappedPrev(i)];
    var corner = corners[i];
    if (prevCorner.x == corner.x)
    {
        var minY = Math.Min(prevCorner.y, corner.y);
        var maxY = Math.Max(prevCorner.y, corner.y);

        for (long j = minY; j <= maxY; j++)
        {
            if (!yToXEdges.ContainsKey(j))
            {
                yToXEdges[j] = new();
            }
            yToXEdges[j].Add(corner.x);
        }
    }
    else
    {
        var minY = Math.Min(prevCorner.y, corner.y);
        var maxY = Math.Max(prevCorner.y, corner.y);

        var minX = Math.Min(prevCorner.x, corner.x);
        var maxX = Math.Max(prevCorner.x, corner.x);

        for (long j = minX; j <= maxX; j++)
        {
            if (!xToYEdges.ContainsKey(j))
            {
                xToYEdges[j] = new();
            }
            xToYEdges[j].Add(corner.y);
        }
    }
}

var isValid2 = ((long x, long y) c1, (long x, long y) c2) =>
{
    var minX = Math.Min(c1.x, c2.x);
    var maxX = Math.Max(c1.x, c2.x);
    var minY = Math.Min(c1.y, c2.y);
    var maxY = Math.Max(c1.y, c2.y);
    if (
        xToYEdges
            .Where(kvp =>
                kvp.Key > minX && kvp.Key < maxX && kvp.Value.Where(y => y > minY && y < maxY).Any()
            )
            .Any()
        || yToXEdges
            .Where(kvp =>
                kvp.Key > minY && kvp.Key < maxY && kvp.Value.Where(x => x > minX && x < maxX).Any()
            )
            .Any()
    )
    {
        return false;
    }
    return true;
};

for (int i = 0; i < lines.Length - 1; i++)
{
    for (int j = i + 1; j < lines.Length; j++)
    {
        var corneri = corners[i];
        var cornerj = corners[j];
        long localArea =
            (Math.Abs(corners[i].x - corners[j].x) + 1)
            * (Math.Abs(corners[i].y - corners[j].y) + 1);

        if (localArea > maxAreaPt1)
        {
            maxAreaPt1 = localArea;
        }

        if (localArea > maxAreaPt2 && isValid2(corners[i], corners[j]))
        {
            maxAreaPt2 = localArea;
        }
    }
}

Console.WriteLine(maxAreaPt1);
Console.WriteLine(maxAreaPt2);
