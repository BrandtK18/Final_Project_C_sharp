

namespace Game
{
    public interface IDamageable
    {
       

        public int Health
        {
            get; set;
        }
        public int TakeDamage(int damage)
        {
            return Health -= damage;
        }
    }
}
