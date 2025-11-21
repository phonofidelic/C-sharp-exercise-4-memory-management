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
                Utils.Clear();
                Utils.WriteLine("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 0) of your choice"
                    + "\n\n1. Examine a List"
                    + "\n2. Examine a Queue"
                    + "\n3. Examine a Stack"
                    + "\n4. CheckParenthesis"
                    + "\n\n0. Exit the application");
                char input = ' '; //Creates the character input to be used with the switch-case below.
                try
                {
                    input = Utils.ReadLine()![0]; //Tries to set input to the first char in an input line
                }
                catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
                {
                    Utils.Clear();
                    Utils.WriteLine("Please enter some input!");
                }
                switch (input)
                {
                    case '1':
                        // LoopUntilExit((action) => ExamineList.Run(action()));
                        ExamineList.Run();
                        break;
                    case '2':
                        ExamineQueue.Run();
                        break;
                    case '3':
                        ExamineStack.Run();
                        break;
                    case '4':
                        CheckParenthesis.Run();
                        break;
                    /*
                     * Extend the menu to include the recursive 
                     * and iterative exercises.
                     */
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        Utils.WriteError("\nPlease enter some valid input (0, 1, 2, 3, 4)");
                        break;
                }
            }
        }
    }
}

