
using Day3;
using System.Diagnostics;

var sw = Stopwatch.StartNew();
string[] lines = Input.INPUT.Split("\r\n");

int countPt1 = 0;
ulong countPt2 = 0;

foreach (string line in lines)
{
    int biggest = 0;
    int second = 0;
    for (int i = 0; i < line.Length; i++)
    {
        int num = int.Parse(line[i].ToString());
        if (num > biggest && i != line.Length - 1)
        {
            biggest = num; second = 0;
        }
        else if (num > second)
        {
            second = num;
        }

    }
    countPt1 += int.Parse(biggest.ToString() + second.ToString());

    string pt2Answer = "";
    int indexOfCurrentBest = 0;

    for (int i = 11; i >= 0; i--)
    {
        
        int currentBest = 0;
        for (int j = indexOfCurrentBest; j < line.Length - i; j++)
        {
            int current = int.Parse(line[j].ToString());
            if (current > currentBest)
            {
                currentBest = current;
                indexOfCurrentBest = j;
            }
        }
        pt2Answer += currentBest.ToString();
        indexOfCurrentBest++;
    }
    countPt2 += ulong.Parse(pt2Answer);
}
sw.Stop();
Console.WriteLine(countPt1);
Console.WriteLine(countPt2);
Console.WriteLine($"Took {sw.ElapsedMilliseconds} ms");