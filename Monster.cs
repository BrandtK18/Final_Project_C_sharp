

namespace Game
{


    public class Monster : IDamageable, IAttack
    {
        int difficulty;
        int health;
        int damage;
        string name;

        // Events
        public event EventHandler SendAttack;

        public Monster(string name, int difficulty, int health, int damage)
        {
            Difficulty = difficulty;
            Health = health;
            Damage = damage;
            Name = name;
        }

        public int Difficulty
        {
            get => difficulty;
            set
            {
                if (value <= 0 || value > 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(difficulty));
                }
                difficulty = value;
            }
        }
        public int Health
        {
            get => health;
            set
            {
                health = value;
            }

        }

        public int Damage
        {
            get => damage;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(damage));
                }
                damage = value;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(name));
                }
                name = value;
            }
        }

        #region Display Methods
        public void PrintStats()
        {
            Console.WriteLine($"{Name} | HP: {Health} / DMG: {Damage}");
        }

        #endregion

        // IAttack
        protected virtual void OnSendAttack(EventArgs e)
        {
            SendAttack?.Invoke(this, e);
        }
        public void Attack()
        {
            Console.WriteLine($"{Name} attacked for {Damage} damage!");
            OnSendAttack(new AttackArgs(Damage));
        }

        // IDamagable
        public void ReceiveAttack(object sender, EventArgs e)
        {
            if (e is AttackArgs a)
                TakeDamage(a.Damage);
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

    }
}
