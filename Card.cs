namespace Game
{
    public abstract class Card
    {
        // Fields
        private int staminaCost;
        
        // Constructors
        public Card() : this(1)
        {

        }
        public Card(int staminaCost)
        {
            StaminaCost = staminaCost;
        }

        // Methods
        public abstract void Use();
        public virtual void Discard()
        {

        }

        // Properties
        public int StaminaCost
        {
            get => staminaCost;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(StaminaCost));
                }
                staminaCost = value;
            }
        }
    }
}