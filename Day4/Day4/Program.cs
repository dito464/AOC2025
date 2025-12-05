using Day4;

string[] lines = Input.INPUT.Split("\r\n");
char[,] rollMap = new char[lines[0].Length, lines.Length];

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        rollMap[x,y] = lines[y][x];
    }
}

var BoundedCheck = (int x, int y) => { 
    if (x < 0 || y < 0 || x >= lines[0].Length || y >= lines.Length || rollMap[x, y] == '.') { return 0; }
    else { return 1; } 
};

int total = 0;

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        int currentCount = 0;
        if (rollMap[x, y] == '@')
        {
            for (int up = -1; up <= 1; up++)
            {
                for (int right = -1; right <= 1; right++)
                {
                    if (!(right == 0 && up == 0))
                    {
                        currentCount += BoundedCheck(x + right, y + up);
                        if (currentCount >= 4)
                        {
                            break;
                        }
                    }

                }
                if (currentCount >= 4) { break; }
            }
            if (currentCount < 4) { total++; }
        }
    }
}

int lastTotal = 0;
int totalpt2 = 0;
do
{
    lastTotal = totalpt2;
    for (int y = 0; y < lines.Length; y++)
    {
        for (int x = 0; x < lines[0].Length; x++)
        {
            int currentCount = 0;
            if (rollMap[x, y] == '@')
            {
                for (int up = -1; up <= 1; up++)
                {
                    for (int right = -1; right <= 1; right++)
                    {
                        if (!(right == 0 && up == 0))
                        {
                            currentCount += BoundedCheck(x + right, y + up);
                            if (currentCount >= 4)
                            {
                                break;
                            }
                        }

                    }
                    if (currentCount >= 4) { break; }
                }
                if (currentCount < 4) { totalpt2++; rollMap[x, y] = '.'; }
            }
        }
    }
} while (lastTotal != totalpt2);

Console.WriteLine(total);
Console.WriteLine(totalpt2);