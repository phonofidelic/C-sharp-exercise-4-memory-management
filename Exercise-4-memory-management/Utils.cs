using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManagement
{
    internal static class Utils
    {
        public static void DisplayDatatypeInfo<T>(IEnumerable<T> data)
        {
            WriteDebug($"Data: [");
            string separator = ", ";

            foreach ((T item, int index) in data.Select((item, index) => (item, index)))
            {
                Write(item);
                if (index < data.Count())
                    WriteDebug(separator);
            }
        }

        public static void Write<T>(T content)
        {
            Console.ResetColor();
            Console.Write(content);
        }

        public static void WriteDebug<T>(T content)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(content);
            Console.ResetColor();
        }

        public static void WriteException(Exception exception) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(exception.Message);
            Console.ResetColor();
        }

        public static void Clear() => Console.Clear();

        public static void ContinueWithKeyPress(bool intercept)
        {
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey(intercept: intercept);
        }
        public static void ContinueWithKeyPress()
        {
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        public static void ContinueWithKeyPress(string message, bool intercept)
        {
            Console.WriteLine(message);
            Console.ReadKey(intercept: intercept);
        }
        public static void ContinueWithKeyPress(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }

}
