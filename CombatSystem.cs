using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Game
{
    public class CombatSystem
    {
        private Monster[] enemies;
        private Random rand = new Random();

        private List<string> log;

        private Player p;
        //private Loot lootSystem = new Loot();

        public CombatSystem(Player p)
        {
            LoadMonster();

            log = new List<string>();
            this.p = p;
        }

        
        public void LoadMonster()
        {
            string path = "MonsterList.csv";
            enemies = new Monster[Game.GetLineCount(path) - 1];

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

        // Combat Specific Methods
        public void StartCombat()
        {
            // Selecting the monster
            int difficulty = 1;

            Monster[] difficultyArray = enemies.Where(m => m.Difficulty == difficulty).ToArray();
            int randIndex = rand.Next(0, difficultyArray.Length);
            
            p.CurrentMonster = difficultyArray[randIndex];

            // Connecting signals
            p.CurrentMonster.SendAttack += p.ReceiveAttack;
            
            p.Reshuffle();
            p.EndTurn();

            for (; ; )
            {
                #region Players turn

                for (; ; )
                {
                    Console.Clear();
                    Display.PrintDisplay([DisplayShortLog, Display.EmptyLine, p.CurrentMonster.PrintStats, Display.EmptyLine, p.PrintStats, p.PrintHand]);
                    Console.WriteLine("e -> end turn | i <index> -> card info | a -> show all cards left in deck");
                    Console.Write("Enter the index of the card you want to play OR menu option: ");

                    try
                    {
                        string input = Console.ReadLine().ToLower();
                        if (input == "e")
                        {
                            p.EndTurn();
                            break;
                        }
                        else if (input == "a")
                        {
                            Console.Clear();
                            p.PrintDeck();
                            Display.AwaitInput();
                            continue;
                        }
                        else if (input[0] == 'i')
                        {
                            string[] values = input.Split(' ');
                            int index = int.Parse(values[1]);

                            Console.Clear();
                            p.PrintCardDescription(index);
                            Display.AwaitInput();

                            continue;
                        }

                        int cardIndex = int.Parse(input);
                        string logString;

                        if (!p.PlayCard(cardIndex, out logString))
                        {
                            Console.WriteLine("You don't have enough stamina to play that card");
                            Display.AwaitInput();
                            continue;
                        }

                        if (logString != "")
                        {
                            log.Add(logString);
                        }

                        if (p.CurrentMonster.Health <= 0)
                        {
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input entered");
                        Display.AwaitInput();
                        continue;
                    }
                }

                #endregion

                if (p.CurrentMonster.Health <= 0)
                {
                    break;
                }

                #region Monsters Turn

                Console.Clear();
                p.CurrentMonster.Attack();
                log.Add($"{p.CurrentMonster.Name} attacked for {p.CurrentMonster.Damage} damage!");
                Display.AwaitInput();

                #endregion
            }

            // Disconnecting signals
            p.CurrentMonster.SendAttack -= p.ReceiveAttack;

            p.CurrentMonster = null;

            // Clear log
            log.Clear();

            // Display
            Console.Clear();
            Console.WriteLine("You defeated the monster!");
            Display.AwaitInput();

            // Loot stuff
        }

        #region Display Methods
        public void DisplayShortLog()
        {
            (int, int) oldCursor = Console.GetCursorPosition();

            Console.WriteLine("= Log ====================");
            Console.SetCursorPosition(oldCursor.Item1, oldCursor.Item2 + 6);
            Console.WriteLine("==========================");
            Console.SetCursorPosition(oldCursor.Item1, oldCursor.Item2 + 1);

            if (log.Count <= 5)
            {
                log.Reverse<string>()
                    .ToList()
                    .ForEach(Console.WriteLine);
            }
            else
            {
                log.Reverse<string>()
                    .Take(5)
                    .ToList()
                    .ForEach(Console.WriteLine);
            }

            Console.SetCursorPosition(oldCursor.Item1, oldCursor.Item2 + 6);
        }

        #endregion

        public Monster[] Enemies
        {
            get => enemies;
        }
    }
}
