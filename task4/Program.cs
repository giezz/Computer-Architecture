using System;
using System.Reflection;

namespace task4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int[,] matrixA = PopulateMatrix(100, 100);
            int[,] matrixB = PopulateMatrix(100, 100);
            
            
            Assembly a = Assembly.Load("task4Library");
            Object o = a.CreateInstance("task4Library.Class1");
            Type t = a.GetType("task4Library.Class1");
            MethodInfo[] mi = t.GetMethods();
            Console.WriteLine(mi[0].Invoke(o, new object[] {matrixA, matrixB}));
            Console.WriteLine(mi[1].Invoke(o, null));
            
            matrixA = PopulateMatrix(1000, 1000);
            matrixB = PopulateMatrix(1000, 1000);
            
            Console.WriteLine(mi[0].Invoke(o, new object[] {matrixA, matrixB}));
            Console.WriteLine(mi[1].Invoke(o, null));
        }

        public static int[,] PopulateMatrix(int dim1, int dim2)
        {
            int[,] matrix = new int[dim1, dim2];
            Random random = new Random();
            for (int i = 0; i < dim1; i++)
            {
                for (int j = 0; j < dim2; j++)
                {
                    matrix[i, j] = random.Next(1000);
                }
            }

            return matrix;
        }
    }
}