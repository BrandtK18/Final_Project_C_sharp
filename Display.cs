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
    }
}
