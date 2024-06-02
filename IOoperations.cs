using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Select_Data
{
    static class IOoperations
    {
        public const string katalogIn = @"_In";       //каталог для обрабатываемых файлов        

        public const string katalogOut = @"_Out";       //каталог для результирующих файлов 

        private static string errorLog = @"errorLog.txt";  //лог с ошибками обработки         

        //------------------------------------------------------------------------------------------
        //создаем каталог
        public static void DirectoryCreater(string createDirectoryName)
        {
            //Создаем пустой каталог
            if (!Directory.Exists(createDirectoryName))
                Directory.CreateDirectory(createDirectoryName);
        }

        //------------------------------------------------------------------------------------------
        //удаляем каталог
        private static void DirectoryDelete(string deleteDirectoryName)
        {
            try
            {
                //Удаляем каталог со всем содержимым 
                if (Directory.Exists(deleteDirectoryName))
                    Directory.Delete(deleteDirectoryName, true);
            }
            catch (IOException ex)
            {
                WriteLogError(ex.ToString());
            }
        }

        //------------------------------------------------------------------------------------------
        //Создаем каталоги по умолчанию, очищаем временные каталоги
        public static void BasicDirectoryAndFileCreate()
        {
            //Создаем каталоги по умолчанию
            DirectoryCreater(katalogIn);            
            DirectoryDelete(katalogOut);
            DirectoryCreater(katalogOut);
        }

        //------------------------------------------------------------------------------------------       
        //Пишем ошибки в лог-файл, по умолчанию @"errorLog.txt"
        public static void WriteLogError(string errormessage)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(errorLog, true, Encoding.GetEncoding(1251)))
                {
                    writer.WriteLine(new string('-', 17));
                    writer.WriteLine(DateTime.Now);
                    writer.WriteLine(new string('-', 17));
                    writer.WriteLine(errormessage);
                    writer.WriteLine(new string('-', 17));
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine();
                Console.WriteLine(new string('-', 17));
                Console.WriteLine("Внимание! Ошибка достаупа к лог-файлу \"errorLog.txt\"");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(new string('-', 17));
            }
        }

    }
}
