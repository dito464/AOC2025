using Day2;

string[] IdPairs = Input.INPUT.Split(',');
ulong count = 0;
ulong count2 = 0;
foreach (string idPair in IdPairs)
{
    string[] idPairsSplit = idPair.Split("-");
    string id1 = idPairsSplit[0];
    string id2 = idPairsSplit[1];
    bool id1OneLength = id1.Length / 2 == 0;
    bool id2OddLength = id2.Length % 2 == 1;
    /*if (id1.Length % 2 == 1 && id1.Length == id2.Length)
    {
        continue;
    }
    else*/
    {
        ulong id1FirstHalf; 
        if (id1OneLength)
        {
            id1FirstHalf = 1;
        }
        else
        {
            id1FirstHalf = ulong.Parse(id1.Substring(0, id1.Length / 2));
        }
        ulong id2FirstHalf = ulong.Parse(id2.Substring(0, id2.Length / 2 + (id2OddLength ? 1 : 0)));

        ulong intId1 = ulong.Parse(id1);
        ulong intId2 = ulong.Parse(id2);

        for (ulong i = id1FirstHalf; i <= id2FirstHalf; i++)
        {
            ulong doubleI = ulong.Parse(i.ToString() + i.ToString());
            if (doubleI >= intId1 && doubleI <= intId2)
            {
                count += (ulong)doubleI;
            }
            else if (doubleI > intId2)
            {
                break;
            }

        }

        HashSet<ulong> list = new HashSet<ulong>();
        for (ulong i = 1; i <= id2FirstHalf; i++) 
        {
            string stringI = i.ToString();
            while (stringI.Length <= id2.Length)
            {
                ulong concatI = ulong.Parse(stringI);
                if (concatI >= intId1 && concatI <= intId2)
                {
                    if (!list.Contains(concatI))
                    {
                        count2 += concatI;
                        list.Add(concatI);
                    }
                }
                stringI += i.ToString();
            }
        }
    }
}

Console.WriteLine(count);
Console.WriteLine(count2);