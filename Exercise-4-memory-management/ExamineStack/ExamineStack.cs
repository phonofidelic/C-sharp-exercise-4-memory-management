using System;


namespace MemoryManagement
{
    /// <summary>
    /// Examines the datastructure Stack
    /// </summary>
    public static class ExamineStack
    {
        public static void Run() 
        {
            Stack<string> stack = [];
            List<int> capacityIncreaseIndexList = [];
            int loopIndex = 0;

            

            // Add some items to the Stack
            for (int i = 1; i <= 5; i++)
            {
                stack.Push($"item {i}");
            }

            ProgramStatus programStatus;
            do
            {
                programStatus = _run(
                    stack,
                    capacityIncreaseIndexList,
                    loopIndex
                );
                if (programStatus.Exception != null)
                {
                    Utils.Clear();
                    Utils.WriteException(new("\nUnhandled error:"));
                    Utils.WriteException(programStatus.Exception);
                    Utils.ContinueWithKeyPress(intercept: true);
                    Utils.Clear();
                    continue;
                }
                loopIndex++;
            } while (programStatus.Code > 0);
        }

        private static ProgramStatus _run(
            Stack<string> stack,
            List<int> capacityIncreaseIndexList,
            int loopIndex
        )
        {
            /*
             * Loop this method until the user inputs something to exit to main menue.
             * Create a switch with cases to push or pop items
             * Make sure to look at the stack after pushing and and poping to see how it behaves
            */
            if (loopIndex == stack.ToList().Capacity)
            {
                capacityIncreaseIndexList.Add(loopIndex);
            }

            (Operation operation, string value) = Utils.TryGetOperationValuePairFromReadLine(() =>
            {
                Console.Clear();
                DisplayProgramIntro();
                Utils.WriteEnumerableInfoWithExtra(stack, capacityIncreaseIndexList, () => DisplayStackInfo(stack));
            });

            try
            {
                switch (operation)
                {
                    case Operation.Exit:
                        Console.Clear();
                        return new ProgramStatus(0);

                    case Operation.Add:
                        Console.Clear();
                        // Call Push to add an item to the top of the Stack
                        stack.Push(value);
                        DisplayProgramIntro();
                        Utils.WriteEnumerableInfoWithExtra(stack, capacityIncreaseIndexList, () => DisplayStackInfo(stack));
                        Utils.WriteInfo("\nPushed: ");
                        Utils.Write($"'{value}'");
                        Utils.WriteLine();

                        break;

                    case Operation.Remove:
                        Console.Clear();
                        DisplayProgramIntro();
                        Utils.WriteEnumerableInfoWithExtra(stack, capacityIncreaseIndexList, () => DisplayStackInfo(stack));
                        // Call Pop to retrieve the item at the top of the Stack
                        var dequeuedValue = stack.Pop();
                        Utils.WriteInfo("\nPopped: ");
                        Utils.Write($"'{dequeuedValue}'");
                        Utils.WriteLine();
                        break;
                    default:
                        throw new InvalidOperationException(operation.ToString());
                }
            }
            catch (Exception ex)
            {
                return new ProgramStatus(-1, ex);
            }

            Utils.ContinueWithKeyPress(intercept: true);

            return new ProgramStatus(1);
        }

        private static void DisplayProgramIntro()
        {
            Utils.WriteLine("Enter '+', then the data you would like to add to the queue.");
            Utils.WriteLine("Enter '-' to retrieve the first item in the queue.");
            Utils.WriteLine("\nEnter 'Q' to exit.\n");
        }

        public static void DisplayStackInfo(Stack<string> stack)
        {
            if (stack.Count > 0)
            {
                Utils.WriteInfo("Next in line: ");
                Utils.Write($"{stack.Peek()}");

                Utils.WriteInfo($"\tCapacity: {stack.ToList().Capacity}");
                Utils.WriteInfo($"\tCount: {stack.Count}");
                Utils.WriteLine("\n");
            }
            else
            {
                Utils.WriteLine("The Queue is empty. Enter text to add more data.");
            }
        }
    }
}
