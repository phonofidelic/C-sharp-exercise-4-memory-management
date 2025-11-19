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
                    capacityIncreaseIndexes.Add(loopIndex);
                }

                DebugListCapacity(theList, capacityIncreaseIndexes);
                PrintListCapacityIntro();

                if (examineListException != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(examineListException.Message);
                    Console.ResetColor();
                }   
                
                // Normalize empty operator to a space character to trigger the error message
                string input = Console.ReadLine() ?? "";
                if (input.Length == 0)
                    input = " ";

                // Normalize empty input to a space character
                char nav = input[0];
                string value = input[1..] ?? " ";
                if (value.Length == 0)
                    value = " ";

                examineListException = null;

                switch (nav)
                {
                    case 'q':
                    case 'Q':
                        goBack = true;
                        break;
                    case '+':
                        Console.Clear();
                        // Print the current list and highlight each item where the Capacity increases.
                        DebugListCapacity(theList, capacityIncreaseIndexes);
                        PrintListCapacityIntro();
                        Console.WriteLine($"Adding '{value}' to the list...");
                        theList.Add(value);
                        loopIndex++;
                        break;

                    case '-':
                        Console.Clear();
                        // The Capacity remains the same when removing items from the List.
                        DebugListCapacity(theList, capacityIncreaseIndexes);
                        PrintListCapacityIntro();
                        Console.WriteLine($"Removing '{value}' from the list...");
                        theList.Remove(value); 
                        loopIndex++;
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
                
            } while (!goBack);
            Console.Clear();
            Program.Main();        
    }
    private static void PrintListCapacityIntro()
        {
            Console.WriteLine("Enter '+' or '-', then the data you would like to add or remove.");
            Console.WriteLine("Enter 'Q' to exit.\n");
        }
        private static void DebugListCapacity(List<string> list, List<int> capacityIncreaseIndexList)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"List: [");
            int i = 0;
            string separator = ", ";
            list.ForEach((content) => {
                if (capacityIncreaseIndexList.Contains(i)){
                    PrintIncrease(content);
                } else
                {
                    PrintContent(content);
                }
                if (i+1 < list.Count) {
                    PrintDebug(separator);
                }
                i++;
            });
            PrintDebug($"]\tCapacity: {list.Capacity}");
            PrintDebug($"\tCount: {list.Count}\n");
            
            // This prints a row showing the indexes where the list Capacity increases
            Console.Write(new string(' ', "List: [".Length));
            foreach ((string item, int index) in list.Select((item, index) => (item, index)))
            {
                int itemWidth = item.Length;
                int separatorWidth = separator.Length;
                int indexWidth = index.ToString().Length;
                if (capacityIncreaseIndexList.Contains(index))
                {
                    Console.Write($"{index}".PadRight(itemWidth + separatorWidth));
                } else
                {
                    Console.Write(new string(' ', itemWidth).PadRight(itemWidth + separatorWidth));
                }
            }
            Console.ResetColor();
            Console.Write("\n\n");
        }
        private static void PrintContent<T>(T content)
        {
            Console.ResetColor();
            Console.Write(content);
        }
        private static void PrintIncrease(string content)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            if (content == " ")
                Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(content);
            Console.ResetColor();
        }
        private static void PrintDebug<T>(T content)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(content);
        }
}
