﻿using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace Game
{
    public class CombatSystem
    {
        private Monster[] enemies;
        private Random rand = new Random();

        private List<string> log;
        
        private Player p;
        private Loot lootSystem;

        public CombatSystem(Player p, Loot lootSystem)
        {
            LoadMonster();

            log = new List<string>();
            this.p = p;
            this.lootSystem = lootSystem;
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
            int difficulty = (p.MonsterCount / 5) + 1;
            if (difficulty > 5)
            {
                difficulty = 5;
            }

            Monster[] difficultyArray = enemies.Where(m => m.Difficulty == difficulty).ToArray();
            int randIndex = rand.Next(0, difficultyArray.Length);
            
            p.CurrentMonster = new Monster(difficultyArray[randIndex]);

            // Connecting signals
            p.CurrentMonster.SendAttack += p.ReceiveAttack;
            Monster current = p.CurrentMonster;
            p.Reshuffle();
            p.EndTurn();

            for (; ; )
            {
                #region Players turn

                if (p.Health <= 0) // If the player dies
                {
                    break;
                }

                for (; ; )
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Display.PrintDisplay([DisplayShortLog, Display.EmptyLine, p.CurrentMonster.PrintStats, Display.EmptyLine, p.PrintStats, p.PrintHand]);
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("e -> end turn | i <index> -> card info | a -> show all cards left in deck | s -> save current game | l -> load game");

                    Console.Write("Enter the index of the card you want to play OR menu option: ");
                    Console.ResetColor();

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
                            Console.ForegroundColor = ConsoleColor.Blue;
                            p.PrintDeck();
                            Console.ResetColor();
                            Display.AwaitInput();
                            continue;
                        }
                        else if (input[0] == 'i')
                        {
                            string[] values = input.Split(' ');
                            int index = int.Parse(values[1]);

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            p.PrintCardDescription(index);
                            Console.ResetColor();
                            Display.AwaitInput();

                            continue;
                        }

                        else if (input == "s")
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            SLED.SaveData(p, current);
                            File.Encrypt("SaveData.csv");
                            SLED.SaveCardData(p, current, p.Cards, p.Discard, p.Hand);
                            Console.ResetColor();
                            Display.AwaitInput();
                            continue;
                        }
                        else if (input == "l")
                        {
                            Console.Clear();
                            SLED.LoadData(p,current);
                            File.Decrypt("SaveData.csv");
                            SLED.LoadCardData(p, current, p.Cards, p.Discard, p.Hand);
                            Display.AwaitInput();
                            continue;
                        }

                        int cardIndex = int.Parse(input);
                        string logString;

                        if (!p.PlayCard(cardIndex, out logString))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("You don't have enough stamina to play that card");
                            Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input entered");
                        Console.ResetColor();
                        Display.AwaitInput();
                        continue;
                    }
                }

                #endregion

                if (p.CurrentMonster.Health <= 0) // If the monster dies
                {
                    break;
                }

                #region Monsters Turn

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                p.CurrentMonster.Attack();
                log.Add($"{p.CurrentMonster.Name} attacked for {p.CurrentMonster.Damage} damage!");
                Console.ResetColor();
                Display.AwaitInput();

                #endregion
            }

            // Disconnecting signals
            p.CurrentMonster.SendAttack -= p.ReceiveAttack;

            p.CurrentMonster = null;

            if (p.Health <= 0) // If the player dies
            {
                return;
            }

            p.MonsterCount += 1;

            // Clear log
            log.Clear();

            // Display
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You defeated the monster!");
            Console.ResetColor();
            Display.AwaitInput();

            // Loot generation
            lootSystem.GenerateLoot(current);
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
