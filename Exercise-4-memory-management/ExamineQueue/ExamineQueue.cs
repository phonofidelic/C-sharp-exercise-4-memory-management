using System;

namespace MemoryManagement;

public static class ExamineQueue
{
    /// <summary>
    /// Examines the datastructure Queue
    /// </summary>
    public static int Run()
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
                Console.Clear();
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
}
