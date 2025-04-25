

namespace Final_Project_C_
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
