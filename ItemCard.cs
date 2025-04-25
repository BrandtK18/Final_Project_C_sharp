namespace Game
{
    public class ItemCard : Card
    {
        // Constructors
        public ItemCard(string name) : this(name, 1, 0, 0)
        {

        }
        public ItemCard(string name, int staminaCost, int health, int stamina) : base(name,staminaCost)
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