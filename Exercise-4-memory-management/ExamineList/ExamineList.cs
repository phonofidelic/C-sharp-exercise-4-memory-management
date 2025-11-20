using System;

namespace MemoryManagement;

/// <summary>
/// Examines the datastructure List
/// </summary>
public static class ExamineList
{
    public static void Run()
    {
        ProgramStatus programStatus;
        // The list to examine
        List<string> theList = [];
        // Keeps track of each Capacity increase
        List<int> capacityIncreaseIndexList = [];
        Exception? examineListException = null;
        int loopIndex = 0;
        do
        {
            programStatus = _run(
                theList,
                capacityIncreaseIndexList,
                examineListException,
                loopIndex
            );
            if (programStatus.Exception != null)

            {
                Utils.Clear();
                if (programStatus.Code < 0)
                    Utils.WriteException(new("\nUnhandled error:"));
                Utils.WriteException(programStatus.Exception);
                Utils.ContinueWithKeyPress(intercept: true);
                Utils.Clear();
                examineListException = null;
                continue;
            }
            loopIndex++;
        } while (programStatus.Code > 0);
        Utils.Clear();
    }

    private static ProgramStatus _run
    (
        List<string> theList,
        List<int> capacityIncreaseIndexes,
        Exception? examineListException,
        int loopIndex
    )
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
                Console.Clear();
                
                // Track each time the list Capacity increases
                if (loopIndex == theList.Capacity)
                {
                    capacityIncreaseIndexes.Add(loopIndex);
                }

                Utils.WriteEnumerableInfo(theList, capacityIncreaseIndexes);

                DisplayProgramIntro();
                string input = Console.ReadLine() ?? "";
                
                // Normalize empty operator to a space character to trigger the error message
                if (input == null || input.Length == 0)
                    input = " ";
                
                // Normalize empty input to a space character
                char nav = input[0];
                string value = input[1..] ?? " ";
                if (value.Length == 0)
                    value = " ";

                switch (nav)
                {
                    case 'q':
                    case 'Q':
                        return new ProgramStatus(0);

                    case '+':
                        Console.Clear();
                        // Print the current list and highlight each item where the Capacity increases.
                        Utils.WriteEnumerableInfo(theList, capacityIncreaseIndexes);
                        DisplayProgramIntro();
                        Utils.WriteInfo($"Adding '{value}' to the list...");
                        theList.Add(value);
                        break;

                    case '-':
                        Console.Clear();
                        // The Capacity remains the same when removing items from the List.
                        Utils.WriteEnumerableInfo(theList, capacityIncreaseIndexes);
                        DisplayProgramIntro();
                        Utils.WriteInfo($"Removing '{value}' from the list...");
                        theList.Remove(value); 
                        break;
                        
                    default:
                        return new ProgramStatus(1, new Exception($"\n'{nav}' is not a valid operation.\nPlease enter '+' or '-', then the data you would like to add or remove.\n"));
                }

                if (examineListException == null)
                {
                    Utils.ContinueWithKeyPress(intercept: true);
                }
                
            return new ProgramStatus(1);      
    }
    private static void DisplayProgramIntro()
    {
        Console.WriteLine("Enter '+' or '-', then the data you would like to add or remove.");
        Console.WriteLine("Enter 'Q' to exit.\n");
    }
    //private static void DisplayListCapacityInfo(List<string> list, List<int> capacityIncreaseIndexList)
    //{

    //    Console.ForegroundColor = ConsoleColor.Cyan;
    //    Console.Write($"List: [");
    //    int i = 0;
    //    string separator = ", ";
    //    list.ForEach((content) => {
    //        if (capacityIncreaseIndexList.Contains(i)){
    //            Utils.WriteIncrease(content);
    //        } else
    //        {
    //            Utils.WriteContent(content);
    //        }
    //        if (i+1 < list.Count) {
    //            Utils.WriteInfo(separator);
    //        }
    //        i++;
    //    });
    //    Utils.WriteInfo($"]\tCapacity: {list.Capacity}");
    //    Utils.WriteInfo($"\tCount: {list.Count}\n");
        
    //    // This prints a row showing the indexes where the list Capacity increases
    //    Console.Write(new string(' ', "List: [".Length));
    //    foreach ((string item, int index) in list.Select((item, index) => (item, index)))
    //    {
    //        int itemWidth = item.Length;
    //        int separatorWidth = separator.Length;
    //        int indexWidth = index.ToString().Length;
    //        if (capacityIncreaseIndexList.Contains(index))
    //        {
    //            Console.Write($"{index}".PadRight(itemWidth + separatorWidth));
    //        } else
    //        {
    //            Console.Write(new string(' ', itemWidth).PadRight(itemWidth + separatorWidth));
    //        }
    //    }
    //    Console.ResetColor();
    //    Console.Write("\n\n");
    //}
}
