using System.Runtime.Serialization;

namespace MemoryManagement
{
    public class InvalidExamineQueueOperationException(string operation) : Exception
    {
        private static string BaseMessage { get; }= "'{0}' is not a valid operation.\nPlease enter '+' or '-', then the data you would like to add or remove.\n";
        public override string Message { get; } = string.Format(BaseMessage, operation);
    }
    /// <summary>
    /// Examines the datastructure Queue
    /// </summary>
    public static class ExamineQueue
    {
        public static void Init()
        {
            Queue<string> queue = [];
            Utils.LoopUntilExit(() => Run(queue));
        }
        
        public static ProgramStatus Run(Queue<string> queue)
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

            Console.Clear();
            DisplayProgramIntro();
            Utils.DisplayEnumerableInfo(queue);

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

                Utils.DisplayEnumerableInfo(queue);
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
                        return new(0);

                    case Operation.Enqueue:
                        Console.Clear();
                        queue.Enqueue(value);
                        DisplayProgramIntro();
                        Utils.DisplayEnumerableInfo(queue);
                        Utils.WriteInfo("\nEnqueued: ");
                        Utils.Write($"'{value}'");
                        Utils.WriteLine();
                
                        break;

                    case Operation.Dequeue:
                        Console.Clear();
                        DisplayProgramIntro();
                        Utils.DisplayEnumerableInfo(queue);
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
                return new(-1, ex);
            }

            Utils.ContinueWithKeyPress(intercept: true);

            return new(1);
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
    }
}
