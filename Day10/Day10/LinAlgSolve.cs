using Google.OrTools.LinearSolver;
using System.Collections;

namespace Day10
{
    public static class LinAlgSolve
    {
        public static int Solve(List<LinAlgInput> input)
        {
            int totalResult = 0;
            foreach (LinAlgInput line in input)
            {
                Solver solver = Solver.CreateSolver("CBC_MIXED_INTEGER_PROGRAMMING");
                Variable[] x = new Variable[line.basis.Count];
                for (int j = 0; j < line.basis.Count; j++)
                {
                    x[j] = solver.MakeIntVar(0, int.MaxValue, $"x[{j}]");
                }

                for (int i = 0; i < line.target.Length; i++)
                {
                    var ct = solver.MakeConstraint(line.target[i], line.target[i], $"row[{i}]");
                    for (int j = 0; j < line.basis.Count; j++)
                    {
                        if (line.basis[j][i] != 0)
                            ct.SetCoefficient(x[j], line.basis[j][i]);
                    }
                }

                var objective = solver.Objective();
                for (int j = 0; j < line.basis.Count; j++) objective.SetCoefficient(x[j], 1);
                objective.SetMinimization();

                var resultStatus = solver.Solve();

                if (resultStatus == Solver.ResultStatus.OPTIMAL || resultStatus == Solver.ResultStatus.FEASIBLE)
                {
                    //Console.WriteLine(solver.Objective().Value());
                    totalResult += (int)solver.Objective().Value();
                }
                else
                {
                    Console.WriteLine("None found - gone wrong");
                }
                
            }
            return totalResult;
        }
    }

    public struct LinAlgInput
    {
        public int[] target;
        public List<int[]> basis;

        public LinAlgInput(int[] target, List<int[]> basis)
        {
            this.target = target;
            this.basis = basis;
        }
    }
}