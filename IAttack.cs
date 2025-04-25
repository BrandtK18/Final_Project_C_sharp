

namespace Game {
    public interface IAttack
    {
        public int Damage
        {
            get;set;
        }
        public void Attack(IDamageable d)
        {
            d.TakeDamage(Damage);
        }

    }
}
