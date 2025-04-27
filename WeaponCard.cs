namespace Game
{
    public class WeaponCard : Card
    {
        // Fields
        private int damage;

        // Constructors
        public WeaponCard(string name) : this(name, 1, 1)
        {

        }
        public WeaponCard(string name, int staminaCost, int damage) : base(name, staminaCost)
        {
            Damage = damage;
        }

        // Methods
        public override void Use()
        {
            Console.WriteLine("Use Weapon!");
            //Attack
        }

        public void Attack(IDamageable a)
        {
            a.TakeDamage(Damage);
        }

        // Properties
        public int Damage
        {
            get => damage;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Damage));
                }
                damage = value;
            }
        }
    }
}