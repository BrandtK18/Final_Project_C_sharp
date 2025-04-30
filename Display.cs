namespace Game
{
    public static class Display
    {
        public static void PrintDisplay(Action[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Invoke();
            }
        }

        public static void AwaitInput()
        {
            Console.WriteLine("(Press Enter to continue)");
            Console.ReadLine();
        }

        public static void EmptyLine()
        {
            Console.WriteLine();
        }
    }
}
