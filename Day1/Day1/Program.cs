using Day1;

string[] lines = Input.INPUT.Split("\r\n");

const int start = 50;
int pos = start;
int prev = pos;
int count = 0;
int pt2count = 0; ;
foreach (var line in lines)
{
    int number = int.Parse(line.Substring(1));
    
    if (line[0] == 'R')
    {
        pos += number;
        while (pos > 99)
        {
            pos -= 100;
            pt2count++;
            
        }
    }
    else if (line[0] == 'L')
    {
        pos -= number;
        while (pos < 0)
        {
            pos = 100 + pos;
            if (prev != 0)
            {
                pt2count++;
            }
            else
            {
                prev = 1; 
            }
        }
    }
    else
    {
        continue;
    }

    if (pos == 0)
    {
        count++;
        if (line[0] == 'L')
        {
            pt2count++;
        }
    }
    prev = pos;
}

Console.WriteLine(count.ToString());
Console.WriteLine(pt2count.ToString());