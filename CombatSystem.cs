namespace Game
{
    public class CombatSystem
    {
        Monster[] enemies;

        public CombatSystem()
        {
            LoadMonster();
        }

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
        public void LoadMonster()
        {
            string path = "MonsterList.csv";
            enemies = new Monster[GetLineCount(path) - 1];

            try
            {
                using StreamReader sr = new StreamReader(path);
                sr.ReadLine();
                for (int i = 0; i < enemies.Length; i++)
                {
                    string line = sr.ReadLine();
                    string[] cols = line.Split(',');

                    string name = cols[0];
                    int difficulty = int.Parse(cols[3]);
                    int health = int.Parse(cols[2]);
                    int damage = int.Parse(cols[1]);

                    enemies[i] = new Monster(name, difficulty, health, damage);
                }
            }
            catch
            {
                Console.WriteLine("Error reading from file", path);
                return;
            }

        }

        public Monster[] Enemies
        {
            get => enemies;
        }
    }
}
