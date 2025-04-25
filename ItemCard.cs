namespace Game
{
    public class ItemCard : Card
    {
        // Constructors
        public ItemCard() : this(0, 0, 1)
        {

        }
        public ItemCard(int health, int stamina, int staminaCost) : base(staminaCost)
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