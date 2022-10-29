using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WinApi;

namespace task1
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Dialogue();
        }

        private static void Dialogue()
        {
            List<string> disks = Api.GetDisks();
            foreach (var disk in disks) 
                Console.Write(disk + " ");

            Console.WriteLine("\nВыберите диск");
            string d = Convert.ToString(Console.ReadLine());
            string selectedDisk = disks
                .Where(p => p.ToLower().StartsWith(d, false, CultureInfo.CurrentCulture))
                .First();

            Console.WriteLine($"Вы выбрали диск {selectedDisk}. Показать все файлы выбранного диска (y/n)?");
            bool chooseOption = false;
            string co = Convert.ToString(Console.ReadLine());

            if (co == "yes" || co == "y") 
                chooseOption = true;
            if (chooseOption) 
                PrintAllFilesWithFileTime(selectedDisk);

            Console.WriteLine("Пропишите путь к файлу/папке у которого хотите изменить временную метку");
            string path = Convert.ToString(Console.ReadLine());
            
            Console.WriteLine("Введите год");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите месяц");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите день");
            int day = Convert.ToInt32(Console.ReadLine());
            
            Api.SetFileTimes(
                path,
                new DateTime(year, month, day),
                new DateTime(year, month, day),
                new DateTime(year, month, day)
            );

            Console.WriteLine("Временная метка изменена");
        }

        private static void PrintAllFilesWithFileTime(string directoryName)
        {
            foreach (var keyValuePair in Api.GetFilesByDirectoryName(directoryName))
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