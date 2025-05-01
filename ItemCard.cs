namespace Game
{
    public class ItemCard : Card
    {
        // Constructors
        public ItemCard(string name) : this(name, 1, "No Description", Rarity.Common, 0, 0)
        {

        }
        public ItemCard(string name, int staminaCost, string description, Rarity rarity, int health, int stamina) : base(name,staminaCost, description, rarity)
        {
            Health = health;
            Stamina = stamina;
        }

        // Methods
        public override void Use()
        {
            
        }
        
        // Properties
        public int Health { get; set; }
        public int Stamina { get; set; }
    }
}