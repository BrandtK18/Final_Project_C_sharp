

namespace Game
{
    public interface IAttack
    {
        event EventHandler SendAttack;

        public int Damage
        {
            get;set;
        }
        void Attack();
    }
}
