using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.AccessControl;
using System.Xml.Linq;
using System.Runtime.ExceptionServices;
using System.Linq;

namespace Game
{
    public class SLED
    {
        public static void SaveData(Player p, Monster m)
        {
            string path = @"SaveData.csv";
            CSVWrite[] playerdata = new CSVWrite[1];
            if (File.Exists(path))
            {
                try
                {
                    using StreamWriter writer = new StreamWriter(path);
                    playerdata[0] = new CSVWrite { HealthMax = p.HealthMax, Health = p.Health, StaminaMax = p.StaminaMax, Stamina = p.Stamina, HandSize = p.HandSize, MonsterCount = p.MonsterCount, Name = m.Name, MHealth = m.Health, Damage = m.Damage, Difficulty = m.Difficulty };
                    writer.WriteLine("HealthMax,Health,StaminaMax,Stamina,MonsterCount,MonsterName,MonsterHealth,MonsterDamage,MonsterDifficulty");
                     
                    for (int i = 0; i < playerdata.Length; i++)
                    {
                        writer.WriteLine(playerdata[i].HealthMax + "," + playerdata[i].Health + "," + playerdata[i].StaminaMax + "," + playerdata[i].Stamina + "," + playerdata[i].HandSize + "," + playerdata[i].MonsterCount + "," + playerdata[i].Name + "," + playerdata[i].MHealth + "," + playerdata[i].Damage + "," + playerdata[i].Difficulty);
                    }

                    Console.WriteLine("Data has been saved");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while writing to file.");
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                File.Create(path);
                Console.WriteLine("File has been created. Exit game and save again to save data.");
            }

            string path2 = @"SaveCardData.csv";
            if (File.Exists(path2))
            {
                try
                {
                    using StreamWriter writer = new StreamWriter(path2);                   
                    writer.WriteLine("Cards,Discard,Hand");
                    foreach ((Card first,Card second,Card third) in p.Cards.Zip(p.Discard,p.Hand))
                    {
                        writer.WriteLine(first.Name + "," + second.Name + "," + third.Name);
                    }

                    Console.WriteLine("Card Data has been saved");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while writing to file.");
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                File.Create(path2);
                Console.WriteLine("Card Data File has been created. Exit game and save again to save data.");
            }
        }

        public static void LoadData(Player p, Monster m)
        {
            string path = "SaveData.csv";

            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            try
            {
                using StreamReader reader = new StreamReader(path);

                int lineCount = GetLineCount(path);
                reader.ReadLine();

                for (int i = 0; i < lineCount - 1; i++)
                {
                    string line = reader.ReadLine();
                    string[] cols = line.Split(',');
                    int pHealthMax = int.Parse(cols[0]);
                    int pHealth = int.Parse(cols[1]);
                    int pStaminaMax = int.Parse(cols[2]);
                    int pStamina = int.Parse(cols[3]);
                    int pHandSize = int.Parse(cols[4]);
                    int pMonCount = int.Parse(cols[5]);
                    string mName = cols[6];
                    int mHealth = int.Parse(cols[7]);
                    int mDamage = int.Parse(cols[8]);
                    int mDifficulty = int.Parse(cols[9]);

                    p.HealthMax = pHealthMax;
                    p.Health = pHealth;
                    p.StaminaMax = pStaminaMax;
                    p.Stamina = pStamina;
                    p.HandSize = pHandSize;
                    p.MonsterCount = pMonCount;

                    m.Name = mName;
                    m.Health = mHealth;
                    m.Damage = mDamage;
                    m.Difficulty = mDifficulty;
                }
                Console.WriteLine("Data has been loaded.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading from file");
                Console.WriteLine(e.Message);
            }
            //string path2 = "SaveCardData.csv";

            //if (!File.Exists(path))
            //{
            //    Console.WriteLine("File does not exist");
            //    return;
            //}

            //try
            //{
            //    using StreamReader reader = new StreamReader(path2);

            //    int lineCount = GetLineCount(path2);
            //    List<Card> cardName = new List<Card>();
            //    List<Card> discardName = new List<Card>();
            //    List<Card> handName = new List<Card>();
            //    reader.ReadLine();

            //    for (int i = 0; i < lineCount - 1; i++)
            //    {
            //        string line = reader.ReadLine();
            //        string[] cols = line.Split(',');
            //        Card cName = (Card)Convert.ChangeType(cols[0], typeof(Card));
            //        cardName.Add(cName);
            //        Card dName = (Card)Convert.ChangeType(cols[1], typeof(Card));
            //        discardName.Add(dName);
            //        Card hName = (Card)Convert.ChangeType(cols[2], typeof(Card));
            //        handName.Add(hName);


            //        p.Cards = cardName;
            //        p.Discard = discardName;
            //        p.Hand = handName;
            //    }
            //    Console.WriteLine("Card Data has been loaded.");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error while reading from file");
            //    Console.WriteLine(e.Message);
            //}
        }


        private static int GetLineCount(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found", path);
            }
            int lines = 0;
            using StreamReader reader = new StreamReader(path);

            while (!reader.EndOfStream)
            {
                reader.ReadLine();
                lines++;
            }
            return lines;

        }
        public class CSVWrite
        {
            public int HealthMax { get; set; }
            public int Health { get; set; }
            public int StaminaMax { get; set; }
            public int Stamina { get; set; }
            public int MonsterCount { get; set; }
            public int HandSize { get; set; }
            public string Name { get; set; }
            public int MHealth { get; set; }
            public int Damage { get; set; }
            public int Difficulty { get; set; }

            //public CSVWrite(int healthmax, int health, int staminaMax, int stamina, int monsterCount, string Name, int mhealth, int damage, int difficulty)
            //{

            //}
        }
        
        public static void EncryptFile(string file)
        {
            File.Encrypt(file);
        }
        public static void DecryptFile(string file)
        {
            File.Decrypt(file);
        }
    }
}
