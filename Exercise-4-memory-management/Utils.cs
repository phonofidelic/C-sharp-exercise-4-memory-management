using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryManagement
{
    internal static class Utils
    {
        public static void DisplayEnumerableInfo<T>(IEnumerable<T> data)
        {
            WriteInfo($"{data.GetType().Name}: [");
            
            string separator = ", ";

            foreach ((T item, int index) in data.Select((item, index) => (item, index)))
            {
                WriteContent(item);
                if (index < data.Count())
                    WriteInfo(separator);
            }
            WriteInfo($"]");
            WriteLine("\n");
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
        public static void LoopUntilExit(Func<ProgramStatus> action)
        {
            ProgramStatus programStatus;
            do
            {
                programStatus = action();
                if (programStatus.Exception != null)
                {
                    Clear();
                    WriteException(new("\nUnhandled error:"));
                    WriteException(programStatus.Exception);
                    ContinueWithKeyPress(intercept: true);
                    Clear();
                }
            } while (programStatus.Code > 0);
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
