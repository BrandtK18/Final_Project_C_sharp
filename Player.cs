namespace Game
{
    public class Player
    {
        List<Card> cards = new List<Card>();
        List<Card> discard = new List<Card>();
        List<Card> hand = new List<Card>();

        private static void DrawCards()
        {

        }
        private static void Reshuffle()
        {

        }
        private static void DrawHand() //draws card 
        {

        }
        private static void AddCard() //adds to top of cards list
        {

        }


        int health;
        int stamina;
        int handSize; // same size as hand list
        int monsterCount;

        public Player()
        {
            Health = health;
            Stamina = stamina;
            HandSize = handSize;
            MonsterCount = monsterCount;
        }

        public int Health
        {
            get => health;
            set
            {
                if(value < 0)
                {
                    throw new Exception("Health cannot be negative");
                }
                value = health;
            }
        }
        public int HandSize
        {
            get => handSize;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Cannot have less than 0 cards in hand");
                }
                value = handSize;
            }
        }
        public int Stamina
        {
            get => stamina;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Stamina cannot be negative");
                }
                value = stamina;
            }
        }
        public int MonsterCount
        {
            get => monsterCount;
            set;
        }
    }
}
