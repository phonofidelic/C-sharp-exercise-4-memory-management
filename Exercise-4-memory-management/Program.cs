using System;

namespace MemoryManagement
{
    class Program
    {
        /// <summary>
        /// The main method, vill handle the menues for the program
        /// </summary>
        /// <param name="args"></param>
        internal static void Main()
        {

            while (true)
            {
                Console.WriteLine("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 0) of your choice"
                    + "\n1. Examine a List"
                    + "\n2. Examine a Queue"
                    + "\n3. Examine a Stack"
                    + "\n4. CheckParenthesis"
                    + "\n0. Exit the application");
                char input = ' '; //Creates the character input to be used with the switch-case below.
                try
                {
                    input = Console.ReadLine()![0]; //Tries to set input to the first char in an input line
                }
                catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
                {
                    Console.Clear();
                    Console.WriteLine("Please enter some input!");
                }
                switch (input)
                {
                    case '1':
                        // LoopUntilExit((action) => ExamineList.Run(action()));
                        ExamineList.Run();
                        break;
                    case '2':
                        LoopUntilExit(ExamineQueue);
                        Console.Clear();
                        Main();
                        break;
                    case '3':
                        ExamineStack();
                        break;
                    case '4':
                        CheckParanthesis();
                        break;
                    /*
                     * Extend the menu to include the recursive 
                     * and iterative exercises.
                     */
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
                        break;
                }
            }
        }

        
        /// <summary>
        /// Examines the datastructure Queue
        /// </summary>
        static int ExamineQueue()
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

            Console.Clear();
            Console.WriteLine("Enter '+' or '-', then the data you would like to add or remove.");
            Console.WriteLine("Enter 'Q' to exit.\n");

            input = Console.ReadLine() ?? "";
            operationInput = input[0];
            
            do
            {
                if (commands.TryGetValue(operationInput, out Operation validOperation))
                {
                    operation = validOperation;
                    break;
                } 

                examineQueueException = new Exception($"\n'{operationInput}' is not a valid operation.\nPlease enter '+' or '-', then the data you would like to add or remove.\n");

                Console.Clear();
                Console.WriteLine("Enter '+' or '-', then the data you would like to add or remove.");
                Console.WriteLine("Enter 'Q' to exit.\n");

                if (examineQueueException != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(examineQueueException.Message);
                    Console.ResetColor();
                }
                input = Console.ReadLine() ?? "";
                
                Console.WriteLine($"ExamineQueue input: {input}");

                operationInput = input[0];
            } while(operation == null);
            
            string value = input[1..];

            Console.Clear();
            Console.WriteLine("Enter '+' or '-', then the data you would like to add or remove.");
            Console.WriteLine("Enter 'Q' to exit.\n");
            Console.WriteLine("\n\n");

            switch (operation)
            {
                case Operation.Exit:
                    return 0;

                case Operation.Enqueue:
                    Console.WriteLine("\nENQUEUE");
                    break;

                case Operation.Dequeue:
                    Console.WriteLine("\nDEQUEUE");
                    break;
                default:
                    examineQueueException = new Exception($"\n'{operationInput}' is not a valid operation.\nPlease enter '+' or '-', then the data you would like to add or remove.\n");
                    break;
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey(intercept: true);

            return 1;
        }

        enum Operation
        {
            Exit,
            Enqueue,
            Dequeue
        };

        static void LoopUntilExit(Func<int> action)
        {
            int programStatus;
            do
            {
                programStatus = action();
            } while (programStatus > 0);
        }

        /// <summary>
        /// Examines the datastructure Stack
        /// </summary>
        static void ExamineStack()
        {
            /*
             * Loop this method until the user inputs something to exit to main menue.
             * Create a switch with cases to push or pop items
             * Make sure to look at the stack after pushing and and poping to see how it behaves
            */
        }

        static void CheckParanthesis()
        {
            /*
             * Use this method to check if the paranthesis in a string is Correct or incorrect.
             * Example of correct: (()), {}, [({})],  List<int> list = new List<int>() { 1, 2, 3, 4 };
             * Example of incorrect: (()]), [), {[()}],  List<int> list = new List<int>() { 1, 2, 3, 4 );
             */

        }

    }
}

