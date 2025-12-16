using Day12;

string[] sections = Input.INPUT.Split("\r\n\r\n");

string[] boxes = new string[sections.Length - 1];
int[] boxCounts = new int[sections.Length - 1];

for (int i = 0; i < sections.Length - 1; i++)
{
    boxes[i] = sections[i];
    boxCounts[i] = sections[i].Where(x => x == '#').Count();
}

string[] puzzles = sections[^1].Split("\r\n");

int valid = 0;
foreach (string puzzle in puzzles)
{
    string[] parts = puzzle.Split(':');
    int[] dimensions = parts[0].Split('x').Select(x => int.Parse(x)).ToArray();
    int[] boxReqs = parts[1].Trim().Split(' ').Select(x => int.Parse(x)).ToArray();
    int totalPresents = boxReqs.Aggregate(0, (total, current) => total + current);
    int totalBoxes = 0;
    for (int i = 0; i < boxReqs.Length; i++)
    {
        totalBoxes += boxReqs[i] * boxCounts[i];
    }

    if (((dimensions[0] / 3) * (dimensions[1] / 3)) >= totalPresents)
    {
        valid++;
    }
    else if (dimensions[0] * dimensions[1] < totalBoxes)
    {
        continue;
    }
    else
    {
        Console.WriteLine("Algorithm needed");
    }
}

Console.WriteLine(valid);
