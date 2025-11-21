using System;

namespace MemoryManagement;

public class CheckParenthesis
{
    public static void Run()
    {
        List<string> testStrings = [
            "([{}]({}))",
            "({)}",
            "List<int> lista = new List<int>(){2, 3, 4};"
        ];

        _run(testStrings);
    }

    private static void _run(List<string> initialStings)
    {
        /*
        * Use this method to check if the parenthesis in a string is Correct or incorrect.
        * Example of correct: (()), {}, [({})],  List<int> list = new List<int>() { 1, 2, 3, 4 };
        * Example of incorrect: (()]), [), {[()}],  List<int> list = new List<int>() { 1, 2, 3, 4 );
        */
        Utils.Clear();
        foreach(string testString in initialStings)
        {
            CheckParens(testString);
        }
        
        Utils.ContinueWithKeyPress();
    }

    private static void CheckParens(string item)
    {
        Utils.WriteInfo("Checking ");
        Utils.Write($"'{item}'");
        Utils.WriteInfo("...\n");
    }
}
