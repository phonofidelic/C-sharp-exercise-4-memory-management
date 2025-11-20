using System;


namespace MemoryManagement
{
    public class LoopCounter
    {
        public int Index { get ; private set;}

        public LoopCounter()
        {
            Index = 0;
        }
        public LoopCounter(int initialIndex)
        {
            Index = initialIndex;
        }

        public int Increment()
        {
            return ++Index;
        }

        public int Decrement()
        {
            return --Index;
        }

        public int Reset()
        {
            return Index = 0;
        }
    }
    /// <summary>
    /// Examines the datastructure Stack
    /// </summary>
    public static class ExamineStack
    {
        public static void Run() 
        {
            LoopCounter loop = new();
            Stack<string> stack = [];
            List<int> capacityIncreaseIndexList = [];
            int startCount = 5;

            

            // Add some items to the Stack
            while(loop.Increment() < startCount)
            {
                stack.Push($"item {loop.Index + 1}");
                if (loop.Index == stack.ToList().Capacity)
                {
                    capacityIncreaseIndexList.Add(loop.Index);
                }
            }

            ProgramStatus programStatus;
            do
            {
                programStatus = _run(
                    stack,
                    capacityIncreaseIndexList,
                    loop
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
            } while (programStatus.Code > 0);
        }

        private static ProgramStatus _run(
            Stack<string> stack,
            List<int> capacityIncreaseIndexList,
            LoopCounter loop
        )
        {
            /*
             * Loop this method until the user inputs something to exit to main menue.
             * Create a switch with cases to push or pop items
             * Make sure to look at the stack after pushing and and poping to see how it behaves
            */
            if (loop.Index == stack.ToList().Capacity)
            {
                capacityIncreaseIndexList.Add(loop.Index);
            }

            (Operation operation, string value) = Utils.TryGetOperationValuePairFromReadLine(() =>
            {
                Console.Clear();
                DisplayProgramIntro();
                Utils.WriteEnumerableInfoWithExtra(stack, capacityIncreaseIndexList, () => DisplayStackInfo(stack, loop.Index));
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
                        loop.Increment();
                        DisplayProgramIntro();
                        Utils.WriteEnumerableInfoWithExtra(stack, capacityIncreaseIndexList, () => DisplayStackInfo(stack, loop.Index));
                        Utils.WriteInfo("\nPushed: ");
                        Utils.Write($"'{value}'");
                        Utils.WriteLine();

                        break;

                    case Operation.Remove:
                        Console.Clear();
                        DisplayProgramIntro();
                        if (value.Equals("empty", StringComparison.OrdinalIgnoreCase))
                        {
                            // Empty the stack if the "empty" command is provided, 
                            // and reset the loopIndex.
                            stack.Clear();
                            loop.Reset();
                            Utils.WriteEnumerableInfoWithExtra(stack, capacityIncreaseIndexList, () => {
                                Utils.WriteLineInfo("Emptying the stack...");
                                DisplayStackInfo(stack, loop.Index);
                            });
                            Utils.WriteLine();
                            capacityIncreaseIndexList.Clear();
                            break;
                        } else
                        {
                            // Call Pop to retrieve the item at the top of the Stack
                            loop.Decrement();
                            var popped = stack.Pop();
                            Utils.WriteEnumerableInfoWithExtra(stack, capacityIncreaseIndexList, () => DisplayStackInfo(stack, loop.Index));
                            Utils.WriteInfo("\nPopped: ");
                            Utils.Write($"'{popped}'");
                            Utils.WriteLine();
                            break;
                        }
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
            Utils.WriteLine("Enter '+', then the data you would like to add to the top of the Stack.");
            Utils.WriteLine("Enter '-' to retrieve the item at the top of the Stack.");
            Utils.WriteLine("\nEnter 'Q' to exit.\n");
        }

        public static void DisplayStackInfo(Stack<string> stack, int loopIndex)
        {
            if (stack.Count > 0)
            {
                Utils.WriteInfo("Next in line: ");
                Utils.Write($"{stack.Peek()}");

                Utils.WriteInfo($"\tCapacity: {stack.ToList().Capacity}");
                Utils.WriteInfo($"\tCount: {stack.Count}");
                Utils.WriteInfo($"\tLoop index: {loopIndex}");
                Utils.WriteLine("\n");
            }
            else
            {
                Utils.WriteLine("The Stack is empty. Enter text to add more data.");
            }
        }
    }
}
