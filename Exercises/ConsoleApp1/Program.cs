using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'cubeSum' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. STRING_ARRAY operations
     */

    public static List<int> cubeSum(int n, List<string> operations)
    {
        long[] x = Enumerable.Range(0, n + 1).Select(x => 0L).ToArray();
        long[] y = Enumerable.Range(0, n + 1).Select(x => 0L).ToArray();
        long[] z = Enumerable.Range(0, n + 1).Select(x => 0L).ToArray();

        List<int> results = new List<int>();

        foreach (var ops in operations)
        {
            var split = ops.Trim().Split(" ");
            var x_num = Convert.ToInt32(split[1]);
            var y_num = Convert.ToInt32(split[2]);
            var z_num = Convert.ToInt32(split[3]);
            if (split[0] == "UPDATE")
            {
                var weight = Convert.ToInt64(split[4]);
                x[x_num] = weight;
                y[y_num] = weight;
                z[z_num] = weight;
            }
            else
            {
                var x1_num = Convert.ToInt32(split[4]);
                var y1_num = Convert.ToInt32(split[5]);
                var z1_num = Convert.ToInt32(split[6]);

                var sum_x = x.Skip(x_num).Take(x1_num - x_num + 1).Sum();
                var sum_y = y.Skip(y_num).Take(y1_num - y_num + 1).Sum();
                var sum_z = z.Skip(z_num).Take(z1_num - z_num + 1).Sum();

                // Console.WriteLine(sum_x);
                results.Add((int)sum_x);
                // Console.WriteLine(sum_y);
                // Console.WriteLine(sum_z);
            }
        }

        return results;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        var fs = File.ReadAllLines("input.in");
        var textWriter = new StreamWriter("output.out", false);

        int T = Convert.ToInt32(fs[0]);
        int counter = 0;

        for (int TItr = 1; TItr < T; TItr++)
        {
            string[] firstMultipleInput = fs[++counter].Split(' ');

            int matSize = Convert.ToInt32(firstMultipleInput[0]);

            int m = Convert.ToInt32(firstMultipleInput[1]);

            List<string> ops = new List<string>();

            for (int i = 0; i < m; i++)
            {
                string opsItem = fs[++counter];
                ops.Add(opsItem);
            }

            List<int> res = Result.cubeSum(matSize, ops);

            textWriter.WriteLine(String.Join("\n", res));
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
