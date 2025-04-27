namespace Game
{
    public class WeaponCard : Card, IAttack
    {
        // Fields
        private int damage;

        // Events
        public event EventHandler SendAttack;

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
            Attack();
        }

        // IAttack
        protected virtual void OnSendAttack(EventArgs e)
        {
            SendAttack?.Invoke(this, e);
        }
        public void Attack()
        {
            OnSendAttack(new AttackArgs(Damage));
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