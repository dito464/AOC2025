using Day7;

string[] lines = Input.INPUT.Split("\r\n");
char[,] manifold = new char[lines[0].Length, lines.Length];
(int x, int y) start = (0, 0);
char startChar = 'S';
char splitterChar = '^';
char emptyChar = '.';

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        manifold[x, y] = lines[y][x];
        if (lines[y][x] == startChar)
        {
            start = (x, y);
        }
    }
}

HashSet<int> lastRow = new HashSet<int>() { start.x };
int count = 0;

for (int y = start.y + 1; y < lines.Length; y++)
{
    HashSet<int> nextRow = new HashSet<int>();
    foreach (int column in lastRow)
    {
        if (manifold[column, y] == splitterChar)
        {
            count++;
            if (column > 0)
            {
                nextRow.Add(column - 1);
            }
            if (column < lines[0].Length - 1)
            {
                nextRow.Add(column + 1);
            }
        }
        else if (manifold[column, y] == emptyChar)
        {
            nextRow.Add(column);
        }
    }
    lastRow = nextRow;
}

Dictionary<(int, int), ulong> cache = new();
ulong totalRoutes(int column, int row)
{
    if (row == lines.Length - 1)
    {
        return 1;
    }

    if (manifold[column, row + 1] == splitterChar)
    {
        ulong localCount = 0;
        if (cache.TryGetValue((column, row + 1), out localCount))
        {
            return localCount;
        }

        if (column > 0)
        {
            localCount += totalRoutes(column - 1, row + 1);
        }
        if (column < lines[0].Length - 1)
        {
            localCount += totalRoutes(column + 1, row + 1);
        }
        cache[(column, row + 1)] = localCount;
        return localCount;
    }
    else if (manifold[column, row + 1] == emptyChar)
    {
        return totalRoutes(column, row + 1);
    }
    return 1;
}

Console.WriteLine(count);
Console.WriteLine(totalRoutes(start.x, start.y));
