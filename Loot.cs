

namespace Game
{
    public class Loot
    {
        //ItemCard healthPot = new ItemCard("Health Potion", 1, 5, 0, "");
        //ItemCard staminaPot = new ItemCard("Stamina Potion", 1, 0, 5, "");
        Loot k = new Loot();
        Player f = new Player(); //dont have access to player 'p' used in main
        public void LootChoice(int c)
        {
            if (c == 0 || c <= 3)
            {
                f.AddCard(healthPot);
            }
            else if (c == 4 || c >= 6)
            {
                f.AddCard(staminaPot);
            }

        }
        public void GenerateLoot()
        {
            Console.WriteLine("Choose your loot!");
            //healthPot.ToString();
            //staminaPot.ToString();
            int i = int.Parse(Console.ReadLine());

            k.LootChoice(i);
        }



    }
}
