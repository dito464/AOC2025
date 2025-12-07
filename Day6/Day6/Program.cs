using Day6;

string[] lines = Input.INPUT.Split("\r\n");
string[] lengthTracker = lines[0].Split(' ').Where(x => x != "").ToArray();
string[,] vals = new string[lengthTracker.Length, lines.Length];

for (int y = 0; y < lines.Length; y++)
{
    string[] line = lines[y].Split(' ').Where(x => x != "").ToArray();
    for (int x = 0; x < line.Length; x++)
    {
        vals[x, y] = line[x];
    }
}

char[,] valspt2 = new char[lines[0].Length, lines.Length];
for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        valspt2[x, y] = lines[y][x];
    }
}

string[,] realValspt2 = new string[lengthTracker.Length, lines[0].Length + 1];
int realyIndex = 0;
int realxIndex = 0;
for (int x = 0; x < lines[0].Length; x++)
{
    string currentNum = "";

    for (int y = 0; y < lines.Length - 1; y++)
    {
        currentNum += lines[y][x];
    }
    long value;
    if (!long.TryParse(currentNum, out value))
    {
        realyIndex = 0;
        realxIndex++;
        continue;
    }
    realValspt2[realxIndex, realyIndex++] = currentNum;
}

int i = 0;
for (int x = 0; x < lines[0].Length; x++)
{
    if (valspt2[x, lines.Length - 1] == '*' || valspt2[x, lines.Length - 1] == '+')
    {
        realValspt2[i++, lines[0].Length] = valspt2[x, lines.Length - 1].ToString();
    }
}

var DoMath = (string[,] values) =>
{
    long total = 0;
    for (int x = 0; x < values.GetLength(0); x++)
    {
        string operation = values[x, values.GetLength(1) - 1];
        long subtotal = 0;
        if (operation == "*")
        {
            subtotal = 1;
        }
        for (int y = 0; y < values.GetLength(1) - 1; y++)
        {
            if (values[x, y] == null)
            {
                continue;
            }
            long currentVal = long.Parse(values[x, y]);
            if (currentVal == -1)
            {
                continue;
            }
            if (operation == "*")
            {
                subtotal *= currentVal;
            }
            else if (operation == "+")
            {
                subtotal += currentVal;
            }
        }
        total += subtotal;
    }
    return total;
};

Console.WriteLine(DoMath(vals));
Console.WriteLine(DoMath(realValspt2));
