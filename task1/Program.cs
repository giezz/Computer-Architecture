using System;
using WinApi;

namespace task1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Console.WriteLine(((WIN32_FIND_DATA) Api.GetFileByPath(@"D:\LZT!")[1]).ftLastAccessTime.dwHighDateTime);
            PrintAllFilesWithFileTime(1);

            //@"D:\testForWinApi"

            Api.SetFileTimes(
                Api.GetFileIntPtr(@"D:\testForWinApi"),
                new DateTime(2015, 01, 01),
                new DateTime(2015, 01, 01),
                new DateTime(2015, 01, 01)
            );
            /*
                Необработанное исключение: 
                System.ComponentModel.Win32Exception: Неверный дескриптор
                в WinApi.Api.SetFileTimes(IntPtr hFile, DateTime creationTime, DateTime accessTime, DateTime writeTime) 
                в C:\Users\Admin\RiderProjects\Computer-Architecture\WinApiClassLibrary\Api.cs:строка 108
                в task1.Program.Main(String[] args) в C:\Users\Admin\RiderProjects\Computer-Architecture\task1\Program.cs:строка 13
            */
        }

        public static void PrintAllFilesWithFileTime(int i)
        {
            foreach (var keyValuePair in Api.GetFilesByDirectoryName(Api.GetDisks()[i]))
            {
                Console.WriteLine(keyValuePair.Key);
                foreach (var valuePair in keyValuePair.Value)
                {
                    Console.WriteLine("\t| " + valuePair.Key + " " + valuePair.Value);
                }
            }
        }
    }
}