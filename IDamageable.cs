

namespace Game
{
    public interface IDamageable
    {
        int Health
        {
            get; set;
        }
        void ReceiveAttack(object sender, AttackArgs e);
    }
}
