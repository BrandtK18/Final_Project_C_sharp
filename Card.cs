namespace Game
{
    public abstract class Card
    {
        // Fields
        private string name;
        private string[] description;
        private int staminaCost;
        
        // Constructors
        public Card(string name) : this(name, 1)
        {

        }
        public Card(string name, int staminaCost)
        {
            Name = name;
            StaminaCost = staminaCost;
        }

        // Methods
        public abstract void Use();
        public virtual void Discard()
        {

        }

        // Properties
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Name), "Name cannot be null or empty");
                }
                name = value;
            }
        }
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