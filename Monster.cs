

namespace Game
{


    public class Monster : IDamageable, IAttack
    {
        int difficulty;
        int health;
        int damage;
        string name;

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

            }
        }
        public int Health
        {
            get => health;
            set
            {

                health = value;
                if (health <= 0)
                {
                    //this is for if the monster dies..
                }
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

        public void Attack(IDamageable a)
        {
            a.TakeDamage(Damage);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

    }
}
