using System;

namespace MemoryManagement;

public class CheckParenthesis
{
    public static void Run()
    {
        List<string> testStrings = [
            "([{}]({}))", // True
            "(()])", // False
            "({)}", // False
            "List<int> lista = new List<int>(){2, 3, 4};" // True
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

    private static bool CheckParens(string item)
    {
        Stack<char> openingParens = [];
        Stack<char> closingParens = [];
        Utils.WriteInfo("Checking ");
        Utils.Write($"'{item}'");
        Utils.WriteInfo("...\n");

        foreach(char c in item)
        {
            if (IsOpeningParen(c))
                openingParens.Push(c);

            if (IsClosingParen(c))
                closingParens.Push(c);
        }

        Utils.WriteInfo("\n\tOpening: [");
        foreach (char c in openingParens.ToList())
            Utils.Write(c);
        Utils.WriteInfo("]");
        Utils.WriteInfo($"\t({openingParens.Count})");

        Utils.WriteInfo("\n\tClosing: [");
        foreach (char c in closingParens.ToList())
            Utils.Write(c);
        Utils.WriteInfo("]");
        Utils.WriteInfo($"\t({closingParens.Count})");
        Utils.WriteLine("\n");

        if (openingParens.Count != closingParens.Count)
        {
            return false;
        }

        return true;
    }

    static bool IsOpeningParen (char c) => "({[<".Contains(c);
    static bool IsClosingParen (char c) => ">]})".Contains(c);


}
