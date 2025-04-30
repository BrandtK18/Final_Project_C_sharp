namespace Game
{
    public static class Game
    {
        private static void Main()
        {
            Player p = new Player();

            // Temporary card creation
            WeaponCard w1 = new WeaponCard("Dagger", 1, 1);
            p.AddCard(w1);
            p.AddCard(w1);
            p.AddCard(w1);
            ItemCard i1 = new ItemCard("Apple", 1, 2, 0);
            p.AddCard(i1);
            p.AddCard(i1);

            CombatSystem cs = new CombatSystem();

            cs.StartCombat(p);
        }
    }
}