namespace Game
{
    public class AttackArgs : EventArgs
    {
        // Fields
        private int damage;

        // Constructor
        public AttackArgs(int damage)
        {
            Damage = damage;
        }

        public int Damage
        {
            get => damage;
            set
            {
                if (damage < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Damage));
                }
                damage = value;
            }
        }
    }
}
