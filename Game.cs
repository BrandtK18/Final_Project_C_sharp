namespace Game
{
    public static class Game
    {
        public static int GetLineCount(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("file does not exist at:" + Path.GetFullPath(path));
                throw new FileNotFoundException("cannot find file", path);
            }
            int count = 0;
            using StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                sr.ReadLine();
                count++;
            }
            return count;
        }

        private static void Main()
        {
            Player p = new Player();
            Loot lootSystem = new Loot(p);
            CombatSystem cs = new CombatSystem(p, lootSystem);

            lootSystem.DirectGainCard(0); // Dagger
            lootSystem.DirectGainCard(0); // Dagger
            lootSystem.DirectGainCard(0); // Dagger
            lootSystem.DirectGainCard(5); // Apple
            lootSystem.DirectGainCard(5); // Apple

            while (p.Health > 0)
            {
                cs.StartCombat();
            }

            Console.Clear();
            Console.WriteLine("You Died!");
            Display.AwaitInput();
        }
    }
}