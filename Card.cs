namespace Game
{
    public abstract class Card
    {
        // Fields
        private string name;
        private string description;
        private int staminaCost;
        
        // Constructors
        public Card(string name) : this(name, 1, "No Description", Rarity.Common)
        {

        }
        public Card(string name, int staminaCost, string description, Rarity rarity)
        {
            Name = name;
            StaminaCost = staminaCost;
            Description = description;
            Rarity = rarity;
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
        public string Description
        {
            get => description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Description), "Description cannot be null or empty");
                }
                description = value;
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
        public Rarity Rarity { get; set; }
    }
}