using System;

namespace MemoryManagement;

public static class ExamineList
{
    /// <summary>
    /// Examines the datastructure List
    /// </summary>
    public static void Run()
    {
            /*
             * Loop this method until the user inputs something to exit to main menu.
             * Create a switch statement with cases '+' and '-'
             * '+': Add the rest of the input to the list (The user could write +Adam and "Adam" would be added to the list)
             * '-': Remove the rest of the input from the list (The user could write -Adam and "Adam" would be removed from the list)
             * In both cases, look at the count and capacity of the list
             * As a default case, tell them to use only + or -
             * Below you can see some inspirational code to begin working.
            */
            List<string> theList = [];
            
            bool goBack = false;
            Exception? examineListException = null;

            // Keeps track of each Capacity increase
            List<int> capacityIncreaseIndexes = [];
            int loopIndex = 0;

            do {
                Console.Clear();
                
                // Track each time the list Capacity increases
                if (loopIndex == theList.Capacity)
                {
                    capacityIncreaseIndexes.Add(theList.Capacity);
                }

                PrintListCapacity(theList, capacityIncreaseIndexes);
                PrintListCapacityIntro();

                if (examineListException != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(examineListException.Message);
                    Console.ResetColor();
                }   
                
                string input = Console.ReadLine() ?? "";
                
                examineListException = null;

                char nav = input[0];
                string value = input[1..];

                switch (nav)
                {
                    case 'q':
                    case 'Q':
                        goBack = true;
                        break;
                    case '+':
                        Console.Clear();
                        // Print the current list and highlight each item where the Capacity increases.
                        PrintListCapacity(theList, capacityIncreaseIndexes);
                        PrintListCapacityIntro();
                        Console.WriteLine($"Adding {value}");
                        theList.Add(value);
                        
                        break;
                    case '-':
                        Console.Clear();
                        // The Capacity remains the same when removing items from the List.
                        PrintListCapacity(theList, capacityIncreaseIndexes);
                        PrintListCapacityIntro();
                        Console.WriteLine($"Removing {value}");
                        theList.Remove(value); 
                        
                        break;
                    default:
                        examineListException = new Exception($"\n'{nav}' is not a valid operation.\nPlease enter '+' or '-', then the data you would like to add or remove.\n");
                        break;
                }

                if (examineListException == null)
                {
                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey(intercept: true);
                }
                loopIndex++;
            } while (!goBack);
            Console.Clear();
            Program.Main();        
    }
    private static void PrintListCapacityIntro()
        {
            Console.WriteLine("Enter '+' or '-', then the data you would like to add or remove.");
            Console.WriteLine("Enter 'Q' to exit.\n");
        }
        private static void PrintListCapacity(List<string> list, List<int> capacityIncreaseIndexes)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"List: [");
            int i = 0;
            list.ForEach((content) => {
                if (capacityIncreaseIndexes.Contains(i)){
                    PrintIncrease(content);
                } else
                {
                    PrintContent(content);
                }
                if (i+1 < list.Count) {
                    PrintDebug(", ");
                }
                i++;
            });
            PrintDebug($"]\tCapacity: {list.Capacity}");
            PrintDebug($"\tCount: {list.Count}");
            Console.Write("\n\n");
            Console.ResetColor();
        }
        private static void PrintContent<T>(T content)
        {
            Console.ResetColor();
            Console.Write(content);
        }
        private static void PrintIncrease<T>(T content)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(content);
            Console.ResetColor();
        }
        private static void PrintDebug<T>(T content)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(content);
        }
}
