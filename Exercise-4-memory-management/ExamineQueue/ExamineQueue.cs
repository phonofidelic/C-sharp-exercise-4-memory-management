namespace MemoryManagement
{
    public static class ExamineQueue
    {
        public static void Init()
        {
            Queue<string> queue = [];
            LoopUntilExit(() => Run(queue));
        }
        /// <summary>
        /// Examines the datastructure Queue
        /// </summary>
        /// 
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
            Exception? examineQueueException;
            Operation? operation = null;
            //Queue<string> queue = [];

            Console.Clear();
            DisplayProgramIntro();
            Utils.DisplayDatatypeInfo(queue);

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

                examineQueueException = new Exception($"'{operationInput}' is not a valid operation.\nPlease enter '+' or '-', then the data you would like to add or remove.\n");

                Console.Clear();
                DisplayProgramIntro();

                if (examineQueueException != null)
                {
                    Utils.WriteException(examineQueueException);
                }

                Utils.DisplayDatatypeInfo(queue);
                input = Console.ReadLine() ?? "";
                if (input == null || input.Length == 0)
                    input = " ";

                operationInput = input[0];
                Console.WriteLine($"ExamineQueue input: {input}");
            } while(operation == null);

            string value = input[1..] ?? " ";
            if (value.Length == 0)
                value = " ";

            Console.Clear();
            DisplayProgramIntro();
            Console.WriteLine("\n\n\n");
            Utils.DisplayDatatypeInfo(queue);

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
                        Utils.DisplayDatatypeInfo(queue);
                        Utils.WriteDebug("\n\nEnqueued: ");
                        Utils.Write(value);
                
                        break;

                    case Operation.Dequeue:
                        Console.Clear();
                        DisplayProgramIntro();
                        Utils.DisplayDatatypeInfo(queue);
                        var dequeuedValue = queue.Dequeue();
                        Utils.WriteDebug("\n\nDequeued: ");
                        Utils.Write(dequeuedValue);
                        break;
                    default:
                        throw new Exception($"'{operationInput}' is not a valid operation.\nPlease enter '+' or '-', then the data you would like to add or remove.\n");
                }
            } catch(Exception ex)
            {
                examineQueueException = ex;
                return new(-1, examineQueueException);
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey(intercept: true);
        
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
            Console.WriteLine("Enter '+' or '-', then the data you would like to add or remove.");
            Console.WriteLine("Enter 'Q' to exit.\n");
        }

        static void LoopUntilExit(Func<ProgramStatus> action)
        {
            ProgramStatus programStatus;
            do
            {
                programStatus = action();
                if (programStatus.Exception != null)
                {
                    Utils.Clear();
                    Utils.WriteException(new("\nUnhandled error:"));
                    Utils.WriteException(programStatus.Exception);
                    Utils.ContinueWithKeyPress(intercept: true);
                    Utils.Clear();
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
