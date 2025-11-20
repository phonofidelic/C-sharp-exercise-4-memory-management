using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManagement
{
    internal static class Utils
    {
        public static void WriteEnumerableInfo(IEnumerable<string> data, List<int> capacityIncreaseIndexList)
        {
            string listLabel = "Data: [";
            Utils.WriteInfo(listLabel);

            string separator = ", ";

            foreach ((string item, int index) in data.Select((item, index) => (item, index)))
            {
                if (capacityIncreaseIndexList.Contains(index))
                {
                    Utils.WriteIncrease(item);
                }
                else
                {
                    Utils.WriteContent(item);
                }
                if (index + 1 < data.ToList().Count)
                    Utils.WriteInfo(separator);
            }
            Utils.WriteInfo($"]\tCapacity: {data.ToList().Capacity}");
            Utils.WriteInfo($"\tCount: {data.ToList().Count}\n");

            // This prints a row showing the indexes where the list Capacity increases
            Console.Write(new string(' ', listLabel.Length));
            foreach ((string item, int index) in data.Select((item, index) => (item, index)))
            {
                int itemWidth = item.Length;
                int separatorWidth = separator.Length;
                int indexWidth = index.ToString().Length;
                if (capacityIncreaseIndexList.Contains(index))
                {
                    Console.Write($"{index}".PadRight(itemWidth + separatorWidth));
                }
                else
                {
                    Console.Write(new string(' ', itemWidth).PadRight(itemWidth + separatorWidth));
                }
            }
            Utils.Write("\n\n");
        }

        public static void WriteEnumerableInfoWithExtra(IEnumerable<string> data, List<int> capacityIncreaseIndexList, Action displayMessage)
        {
            WriteEnumerableInfo(data, capacityIncreaseIndexList);
            displayMessage();

        }

        public static void WriteLineInfo(string message)
        {

        }

        public static void WriteLine(string message) => Console.WriteLine(message);
        public static void WriteLine() => Console.WriteLine();
        public static void Write<T>(T content)
        {
            Console.ResetColor();
            Console.Write(content);
        }

        public static void WriteContent<T>(T content)
        {
            Console.ResetColor();
            Console.Write(content);
        }

        public static void WriteInfo(string content)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(content);
            Console.ResetColor();
        }

        public static void WriteIncrease(string content)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            if (content == " ")
                Console.ForegroundColor = ConsoleColor.Green;
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
            Console.CursorVisible = false;
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey(intercept: intercept);
            Console.CursorVisible = true;
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

    public class ProgramStatus
    {
        public int Code { get; private set; }
        public Exception? Exception { get; private set; }

        public ProgramStatus(int code, Exception exception)
        {
            Code = code;
            Exception = exception;
        }

        public ProgramStatus(int code)
        {
            Code = code;
            Exception = null;
        }
    }
}
