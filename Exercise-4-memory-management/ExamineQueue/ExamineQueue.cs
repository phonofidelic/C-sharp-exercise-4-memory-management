using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MemoryManagement
{    
    /// <summary>
    /// Examines the datastructure Queue
    /// </summary>
    public static class ExamineQueue
    {
        public static void Run()
        {
            Queue<string> queue = [];
            List<int> capacityIncreaseIndexList = [];
            int loopIndex = 0;

            // Add some items to the Queue
            for (int i = 1; i <= 5; i++)
            {
                queue.Enqueue($"item {i}");
            }

            ProgramStatus programStatus;
            do
            {
                programStatus = _run(
                    queue,
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
            Queue<string> queue,
            List<int> capacityIncreaseIndexList,
            int loopIndex
        )
        {
            Dictionary<char, Operation> commands = [];
            commands.Add('q', Operation.Exit);
            commands.Add('Q', Operation.Exit);
            commands.Add('+', Operation.Enqueue);
            commands.Add('-', Operation.Dequeue);
            /*
            * Loop this method until the user inputs something to exit to main menu.
            * Create a switch with cases to enqueue items or dequeue items
            * Make sure to look at the queue after Enqueueing and Dequeueing to see how it behaves
            */
            string input;
            char operationInput;
            InvalidExamineQueueOperationException? examineQueueException;
            Operation? operation = null;

            if (loopIndex == queue.ToList().Capacity)
            {
                capacityIncreaseIndexList.Add(loopIndex);
            }

            Console.Clear();
            DisplayProgramIntro();
            Utils.WriteEnumerableInfoWithExtra(queue, capacityIncreaseIndexList, () => DisplayQueueInfo(queue));

            // Normalize empty operator to a space character to trigger the error message
            input = Console.ReadLine() ?? "";
            if (input == null || input.Length == 0)
                input = " ";

            operationInput = input[0];
        
            do
            {
                if (commands.TryGetValue(operationInput, out Operation validOperation))
                {
                    operation = validOperation;
                    break;
                } 

                examineQueueException = new InvalidExamineQueueOperationException(operationInput.ToString());

                Console.Clear();
                DisplayProgramIntro();

                if (examineQueueException != null)
                {
                    Utils.WriteException(examineQueueException);
                }

                Utils.WriteEnumerableInfoWithExtra(queue, capacityIncreaseIndexList, () => DisplayQueueInfo(queue));
                input = Console.ReadLine() ?? "";
                if (input == null || input.Length == 0)
                    input = " ";

                operationInput = input[0];
            } while(operation == null);

            string value = input[1..] ?? " ";
            if (value.Length == 0)
                value = " ";

            try
            {
                switch (operation)
                {
                    case Operation.Exit:
                        Console.Clear();
                        return new ProgramStatus(0);

                    case Operation.Enqueue:
                        Console.Clear();
                        queue.Enqueue(value);
                        DisplayProgramIntro();
                        Utils.WriteEnumerableInfoWithExtra(queue, capacityIncreaseIndexList, () => DisplayQueueInfo(queue));
                        Utils.WriteInfo("\nEnqueued: ");
                        Utils.Write($"'{value}'");
                        Utils.WriteLine();
                
                        break;

                    case Operation.Dequeue:
                        Console.Clear();
                        DisplayProgramIntro();
                        Utils.WriteEnumerableInfoWithExtra(queue, capacityIncreaseIndexList, () => DisplayQueueInfo(queue));
                        var dequeuedValue = queue.Dequeue();
                        Utils.WriteInfo("\nDequeued: ");
                        Utils.Write($"'{dequeuedValue}'");
                        Utils.WriteLine();
                        break;
                    default:
                        throw new InvalidExamineQueueOperationException(operationInput.ToString());
                }
            } catch(Exception ex)
            {
                return new ProgramStatus(-1, ex);
            }

            Utils.ContinueWithKeyPress(intercept: true);

            return new ProgramStatus(1);
        }
        enum Operation
        {
            Exit,
            Enqueue,
            Dequeue
        };

        private static void DisplayProgramIntro()
        {
            Utils.WriteLine("Enter '+', then the data you would like to add to the queue.");
            Utils.WriteLine("Enter '-' to retrieve the first item in the queue.");
            Utils.WriteLine("\nEnter 'Q' to exit.\n");
        }

        public static void DisplayQueueInfo(Queue<string> queue)
        {
            //string listLabel = "Queue: [";
            //Utils.WriteInfo(listLabel);

            //string separator = ", ";

            //foreach ((var item, int index) in queue.Select((item, index) => (item, index)))
            //{
            //    if (capacityIncreaseIndexList.Contains(index))
            //    {
            //        Utils.WriteIncrease(item);
            //    }
            //    else
            //    {
            //        Utils.WriteContent(item);
            //    }
            //    if (index + 1 < queue.Count)
            //        Utils.WriteInfo(separator);
            //}
            //Utils.WriteInfo($"]\tCapacity: {queue.ToList().Capacity}");
            //Utils.WriteInfo($"\tCount: {queue.Count}\n");

            //// This prints a row showing the indexes where the list Capacity increases
            //Console.Write(new string(' ', listLabel.Length));
            //foreach ((string item, int index) in queue.Select((item, index) => (item, index)))
            //{
            //    int itemWidth = item.Length;
            //    int separatorWidth = separator.Length;
            //    int indexWidth = index.ToString().Length;
            //    if (capacityIncreaseIndexList.Contains(index))
            //    {
            //        Console.Write($"{index}".PadRight(itemWidth + separatorWidth));
            //    }
            //    else
            //    {
            //        Console.Write(new string(' ', itemWidth).PadRight(itemWidth + separatorWidth));
            //    }
            //}
            //Utils.Write("\n\n");
            if (queue.Count > 0)
            {
                Utils.WriteInfo("Next in line: ");
                Utils.Write($"{queue.Peek()}");

                Utils.WriteInfo($"\tCapacity: {queue.ToList().Capacity}");
                Utils.WriteInfo($"\tCount: {queue.Count}");
                Utils.WriteLine("\n");
            }
            else
            {
                Utils.WriteLine("The Queue is empty. Enter text to add more data.");
            }
        }
    }

    

    public class InvalidExamineQueueOperationException(string operation) : Exception
    {
        private static string BaseMessage { get; }= "'{0}' is not a valid operation.\nPlease enter '+' or '-', then the data you would like to add or remove.\n";
        public override string Message { get; } = string.Format(BaseMessage, operation);
    }
}
