namespace Game
{
    public class Player
    {
        List<Card> cards = new List<Card>();
        List<Card> discard = new List<Card>();
        List<Card> hand = new List<Card>();

        Random rand = new Random();
        private static void DrawCard() //takes card from top of cards list and puts it into hand
        {
            Card c = cards(cards.Size - 1);
            hand.add(c);
            cards.RemoveAt(cards.Size - 1);

        }
        private static void Reshuffle() //puts random discards back into cards
        {
            while(cards.Size > 0)
            {
                DrawCard();
            }
            while(discard.Size > 0)
            {
                cards.Add(discard.Next(0, cards.Size));
                discard.RemoveAt(discard.Next(0,cards.Size));
            }
        }
        private static void DrawHand()
        {
            while(hand.Size < handSize)
            {
                DrawCard();
            }
        }
        private static void AddCard() //adds to cards list
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
            get;
            set;
        }
    }
}
