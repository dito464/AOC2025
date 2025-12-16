using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Day10;
using Google.OrTools.LinearSolver;

string[] lines = Input.INPUT.Split("\r\n");

Stopwatch sw = Stopwatch.StartNew();
List<Line> pt1Input = new();
List<LinAlgInput> pt2Input = new();

foreach (string line in lines)
{
    string[] input = line.Split(' ');
    string lights = input[0];
    BitArray lightArray = new(lights.Length - 2);
    for (int i = 0; i < lights.Length - 2; i++)
    {
        if (lights[i + 1] == '#')
        {
            lightArray[i] = true;
        }
        else
        {
            lightArray[i] = false;
        }
    }

    string joltage = input[^1];
    joltage = joltage.Trim('{').Trim('}');
    string[] jolts = joltage.Split(',');
    int[] intJolts = new int[jolts.Length];
    for (int i = 0; i < intJolts.Length; i++)
    {
        intJolts[i] = int.Parse(jolts[i]);
    }

    List<int[]> buttons = new();
    List<BitArray> buttonsArr = new();
    for (int i = 1; i < input.Count() - 1; i++)
    {
        string button = input[i];
        button = button.Trim('(').Trim(')');

        string[] buttonLights = button.Split(",");
        int[] buttonLightInts = new int[jolts.Length];
        BitArray buttonLightIntsArray = new BitArray(jolts.Length);
        foreach (var buttonLight in buttonLights)
        {
            int index = int.Parse(buttonLight);
            buttonLightInts[index] = 1;
            buttonLightIntsArray[index] = true;
        }
        buttons.Add(buttonLightInts);
        buttonsArr.Add(buttonLightIntsArray);
    }
    Line pt1 = new Line(lightArray, buttonsArr);
    LinAlgInput pt2 = new LinAlgInput(intJolts, buttons);
    pt1Input.Add(pt1);
    pt2Input.Add(pt2);
}

Console.WriteLine($"Parsing took {sw.ElapsedMilliseconds}ms");
sw = Stopwatch.StartNew();
int buttonsPressed = 0;

foreach (Line line in pt1Input)
{
    if (!line.lights.Cast<bool>().Any(b => b))
    {
        continue;
    }
    List<LineAttempt> candidates = new();
    foreach (var button in line.buttons)
    {
        BitArray tmpButton = (BitArray)button.Clone();
        if (tmpButton.And(line.lights).Cast<bool>().Any(x => x))
        {
            candidates.Add(new LineAttempt((BitArray)line.lights.Clone(), button));
        }
    }
    List<LineAttempt> newCandidates;

    while (candidates.Count > 0) // while true
    {
        buttonsPressed++;
        bool found = false;
        foreach (var candidate in candidates)
        {
            candidate.lights.Xor(candidate.button);

            if (!candidate.lights.Cast<bool>().Any(b => b))
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            break;
        }

        newCandidates = new();
        foreach (var candidate in candidates)
        {
            foreach (var button in line.buttons)
            {
                BitArray tmpButton = (BitArray)button.Clone();
                if (tmpButton.And(candidate.lights).Cast<bool>().Any(x => x))
                {
                    newCandidates.Add(new LineAttempt((BitArray)candidate.lights.Clone(), button));
                }
            }
        }
        candidates = newCandidates;
    }
}
Console.WriteLine(buttonsPressed);
Console.WriteLine($"Part 1 took {sw.ElapsedMilliseconds}ms");

sw = Stopwatch.StartNew();
Console.WriteLine(LinAlgSolve.Solve(pt2Input));
Console.WriteLine($"Part 2 took {sw.ElapsedMilliseconds}ms");
