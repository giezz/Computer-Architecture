using System;
using System.Drawing;
using System.Reflection;
using task4Library;

namespace task4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            DynamicLoad();
            StaticLoad();
        }

        private static void StaticLoad()
        {
            ClassForStaticLoad.PaintLine3(
                new IntPtr(0),
                new Point(0, 0),
                new Point(ClassForStaticLoad.GetMonitorResolution()[0], ClassForStaticLoad.GetMonitorResolution()[1]),
                false
            );
            ClassForStaticLoad.PaintLine3(
                new IntPtr(0),
                new Point(ClassForStaticLoad.GetMonitorResolution()[0], 0),
                new Point(0, ClassForStaticLoad.GetMonitorResolution()[1]),
                false
            );
        }


        private static void DynamicLoad()
        {
            int[,] matrixA = PopulateMatrix(100, 100);
            int[,] matrixB = PopulateMatrix(100, 100);


            Assembly a = Assembly.Load("task4Library");
            Object o = a.CreateInstance("task4Library.ClassForDynamicLoad");
            Type t = a.GetType("task4Library.ClassForDynamicLoad");
            MethodInfo multiplicationMi = t.GetMethod("Multiplication", BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo getTimeMi = t.GetMethod("GetTime", BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine(multiplicationMi.Invoke(o, new object[] {matrixA, matrixB}));
            Console.WriteLine(getTimeMi.Invoke(o, null));
            
            matrixA = PopulateMatrix(1000, 1000);
            matrixB = PopulateMatrix(1000, 1000);
            
            Console.WriteLine(multiplicationMi.Invoke(o, new object[] {matrixA, matrixB}));
            Console.WriteLine(getTimeMi.Invoke(o, null));
        }

        private static int[,] PopulateMatrix(int dim1, int dim2)
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