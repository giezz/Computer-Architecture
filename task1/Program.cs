using System;
using WinApi;

namespace task1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Api.GetFileByPath(@"D:\LZT!")[1]);
        }

        public static void PrintAllFilesWithFileTime()
        {
            foreach (var keyValuePair in Api.Foo(Api.GetDisks()[1]))
            {
                Console.WriteLine(keyValuePair.Key);
                foreach (var valuePair in keyValuePair.Value)
                {
                    Console.WriteLine("\t| " + valuePair.Key + " " + valuePair.Value.dwLowDateTime);
                }
            }
        }
    }
}