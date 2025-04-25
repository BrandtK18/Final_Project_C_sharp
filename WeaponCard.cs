namespace Game
{
    public class WeaponCard : Card, IAttack
    {
        // Fields
        private int damage;

        // Constructors
        public WeaponCard() : this(1, 1)
        {

        }
        public WeaponCard(int staminaCost, int damage) : base(staminaCost)
        {
            Damage = damage;
        }

        // Methods
        public override void Use()
        {
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