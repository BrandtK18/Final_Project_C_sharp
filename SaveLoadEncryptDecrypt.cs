using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.AccessControl;

namespace Game
{
    public class SLED
    {
        public static void SaveData(Player p, Monster m)
        {
            string path = "SaveData.txt";

            if (File.Exists(path))
            {
                try
                {
                    using StreamWriter writer = new StreamWriter(path);
                    writer.WriteLine(p.Health);
                    writer.WriteLine(p.Stamina);
                    writer.WriteLine(p.MonsterCount);
                    writer.WriteLine("Cards:");
                    foreach(Card c in p.Cards)
                    {
                        writer.WriteLine(c.Name);
                    }
                    writer.WriteLine("Discard:");
                    foreach(Card d in p.Discard)
                    {
                        writer.WriteLine(d.Name);
                    }
                    writer.WriteLine("Hand:");
                    foreach(Card h in p.Hand)
                    {
                        writer.WriteLine(h.Name);
                    }
                    writer.WriteLine("Monster:");
                    writer.WriteLine(m.Name);
                    writer.WriteLine(m.Health);
                    writer.WriteLine(m.Damage);
                    writer.WriteLine(m.Difficulty);
                    Console.WriteLine("Data has been saved.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while writing to file");
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                File.Create(path);
                Console.WriteLine("File created. Save again to write data to file.");
            }
            
        }

        public static void LoadData(Player p, Monster m)
        {
            string path = "SaveData.txt";

            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            try
            {
                using StreamReader reader = new StreamReader(path);
                List<string> sCards = new List<string>();
                List<string> sDiscard = new List<string>();
                List<string> sHand = new List<string>();
                List<Card> nCards = sCards.OfType<Card>().ToList();
                List<Card> nDiscard = sDiscard.OfType<Card>().ToList();
                List<Card> nHand = sHand.OfType<Card>().ToList();
                while (!reader.EndOfStream)
                {
                    string pHealth = reader.ReadLine();
                    string pStamina = reader.ReadLine();
                    string pMonCount = reader.ReadLine();
                    if(reader.ReadLine() == "Cards:")
                    {
                        continue;
                    }
                    while(reader.ReadLine() != "Discard:")
                    {
                        sCards.Add(reader.ReadLine());
                    }
                    if(reader.ReadLine() == "Discard:")
                    {
                        continue;
                    }
                    while(reader.ReadLine() != "Hand:")
                    {
                        sDiscard.Add(reader.ReadLine());
                    }
                    if(reader.ReadLine() == "Hand:")
                    {
                        continue;
                    }
                    while(reader.ReadLine() != "Monster:")
                    {
                        sHand.Add(reader.ReadLine());
                    }
                    if(reader.ReadLine() == "Monster:")
                    {
                        continue;
                    }
                    string mName = reader.ReadLine();
                    string mHealth = reader.ReadLine();
                    string mDamage = reader.ReadLine();
                    string mDifficulty = reader.ReadLine();

                    p.Health = Convert.ToInt32(pHealth);
                    p.Stamina = Convert.ToInt32(pStamina);
                    p.MonsterCount = Convert.ToInt32(pMonCount);

                    p.Cards = nCards;
                    p.Discard = nDiscard;
                    p.Hand = nHand;

                    m.Name = mName;
                    m.Health = Convert.ToInt32(mHealth);
                    m.Damage = Convert.ToInt32(mDamage);
                    m.Difficulty = Convert.ToInt32(mDifficulty);

                }
                Console.WriteLine("Data has been loaded.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading from file");
                Console.WriteLine(e.Message);
            }
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
