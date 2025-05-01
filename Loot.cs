

using System.IO;

namespace Game
{
    public class Loot
    {
        // Fields
        private const string card_path = "cards.csv";

        private Card[] allCards;
        private Player p;

        // Constructor
        public Loot(Player p)
        {
            this.p = p;
            LoadCards();
        }

        // Methods
        private void LoadCards()
        {
            allCards = new Card[Game.GetLineCount(card_path) - 1];

            try
            {
                using StreamReader input = new StreamReader(card_path);
                input.ReadLine();
                for (int i = 0; i < allCards.Length; i++)
                {
                    string line = input.ReadLine();
                    string[] values = line.Split(',');

                    string name = values[1];
                    int staminaCost = int.Parse(values[2]);
                    string description = values[3];
                    Rarity rarity = (Rarity)int.Parse(values[4]);

                    if (values[0] == "Weapon")
                    {
                        int damage = int.Parse(values[5]);

                        WeaponCard c = new WeaponCard(name, staminaCost, description, rarity, damage);
                        AllCards[i] = c;
                    }
                    else if (values[0] == "Item")
                    {
                        int health = int.Parse(values[5]);
                        int stamina = int.Parse(values[6]);

                        ItemCard c = new ItemCard(name, staminaCost, description, rarity, health, stamina);
                        AllCards[i] = c;
                    }
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Error reading from card file");
                Display.AwaitInput();
                return;
            }
        }
        
        public void GenerateLoot()
        {
            // Generate the loot table using the Card array and Rarity Enum (I would recommend using LINQ for selecting / sorting by rarity etc.)
            // I would say randomly (with rarities in mind) pick 3 cards for the user to choose from


            // Then have the user select one of the cards from a list


            // Then add that card to the players deck (Don't use the DirectGainCard Method use p.AddCard instead)
            
        }
        public void DirectGainCard(int card_index) // THIS IS FOR GIVING THE PLAYER THEIR INITIAL STARTING CARDS
        {
            p.AddCard(allCards[card_index]);
        }

        // Properties
        public Card[] AllCards
        {
            get => allCards;
            set => allCards = value;
        }
    }
}
