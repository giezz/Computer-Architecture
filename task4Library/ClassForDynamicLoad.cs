using System;
using System.Diagnostics;

namespace task4Library
{
    public class ClassForDynamicLoad
    {
        private long multiplicationTime;
        private Stopwatch stopwatch;

        private int[,] Multiplication(int[,] a, int[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            int[,] resultMatrix = new int[a.GetLength(0), b.GetLength(1)];
            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        resultMatrix[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            stopwatch.Stop();
            multiplicationTime = stopwatch.ElapsedMilliseconds;
            
            return resultMatrix;
        }

        private long GetTime()
        {
            return multiplicationTime;
        }
    }
}