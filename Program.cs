using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Select_Data
{
    class Program
    {
        private static DateTime start;        

        static void Main(string[] args)
        {
            //время начала обработки
            start = DateTime.Now;

            //Создаем каталоги по умолчанию
            IOoperations.BasicDirectoryAndFileCreate();

            //обрабатываем файлы в каталоге
            Console.WriteLine(new string('-', 91));
            Console.WriteLine();
            Console.WriteLine("Началась обработка файлов в каталоге \"_In\", пожалуйста ждите...");
            Console.WriteLine();
            
            SelectData.ObrFileFromDirectory(IOoperations.katalogIn);

            Console.WriteLine();
            
            //вычисляем время затраченное на обработку
            TimeSpan stop = DateTime.Now - start;

            Console.WriteLine(new string('-', 91));
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Обработка выполнилась за " + stop.TotalSeconds + " секунд.");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadKey();
        }
    }
}
