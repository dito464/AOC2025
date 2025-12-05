using System.Collections.Specialized;
using Day5;

string[] lines = Input.INPUT.Split("\r\n");
SortedDictionary<ulong, ulong> valid = new();
bool ranges = true;
ulong count = 0;
foreach (string line in lines)
{
    if (line == "")
    {
        ranges = false;
        continue;
    }

    if (ranges)
    {
        string[] ends = line.Split('-');
        ulong start = ulong.Parse(ends[0]);
        ulong end = ulong.Parse(ends[1]);

        var overlapping_starts = valid.Where(x => x.Key >= start && x.Key <= end);
        bool added = false;

        if (overlapping_starts.Any())
        {
            ulong max_end = overlapping_starts.Last().Value;
            foreach (var entry in overlapping_starts.ToList())
            {
                valid.Remove(entry.Key);
            }
            if (end >= max_end)
            {
                max_end = end;
            }
            valid.Add(start, max_end);
            end = max_end;
            added = true;
        }

        var overlapping_ends = valid.Where(x => x.Value >= start && x.Value <= end);
        if (overlapping_ends.Any())
        {
            ulong min_start = overlapping_ends.First().Key;
            foreach (var entry in overlapping_ends.ToList())
            {
                valid.Remove(entry.Key);
            }
            if (start <= min_start)
            {
                min_start = start;
            }
            valid.Add(min_start, end);
            added = true;
        }

        if (!added && !valid.Where(x => x.Key <= start && x.Value >= end).Any())
        {
            valid.Add(start, end);
        }
    }
    else
    {
        ulong value = ulong.Parse(line);
        if (valid.Where(x => x.Key <= value && x.Value >= value).Any())
        {
            count++;
        }
    }
}
ulong total_valid = 0;
foreach (var entry in valid)
{
    total_valid += entry.Value - entry.Key + 1;
}

Console.WriteLine(count);
Console.WriteLine(total_valid);
