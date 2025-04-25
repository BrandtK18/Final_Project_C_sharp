

namespace Game
{
    public class Monster : IDamageable, IAttack
    {
        int difficulty;
        int health;
        int damage;

        public Monster(int difficulty, int health, int damage)
        {
            Difficulty = difficulty;
            Health = health;
            Damage = damage;
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

    }
}
