namespace Game
{
    public class Player
    {
        private List<Card> cards;
        private List<Card> discard;
        private List<Card> hand;

        private Random rand = new Random();
       
        private int health;
        private int stamina;
        private int handSize; // same size as hand list

        public Player() : this(10, 3, 3, 0)
        {

        }
        public Player(int health, int stamina, int handSize, int monsterCount)
        {
            Health = health;
            Stamina = stamina;
            HandSize = handSize;
            MonsterCount = monsterCount;

            cards = new List<Card>();
            discard = new List<Card>();
            hand = new List<Card>();
        }

        public void DrawCard() //takes card from top of cards list and puts it into hand
        {
            Card c = cards[cards.Count - 1];
            hand.Add(c);
            cards.RemoveAt(cards.Count - 1);

        }
        public void Reshuffle() //puts random discards back into cards
        {
            while (cards.Count > 0)
            {
                DrawCard();
            }
            while (discard.Count > 0)
            {
                int rand_index = rand.Next(0, discard.Count);
                cards.Add(discard[rand_index]);
                discard[rand_index] = discard[discard.Count - 1];
                discard.RemoveAt(discard.Count - 1);
            }
        }
        public void DrawHand()
        {
            while (hand.Count < handSize)
            {
                DrawCard();
            }
        }
        public void AddCard(Card c) //adds to cards list
        {
            cards.Add(c);
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
