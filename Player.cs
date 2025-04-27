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
            hand.Add(cards.Last());
            cards.RemoveAt(cards.Count - 1);
        }
        public void Reshuffle() //puts random discards back into cards
        {
            while (cards.Count > 0)
            {
                discard.Add(cards.Last());
                cards.RemoveAt(cards.Count - 1);
            }
            while (discard.Count > 0)
            {
                int rand_index = rand.Next(0, discard.Count);
                cards.Add(discard[rand_index]);
                discard[rand_index] = discard.Last();
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
        public bool PlayCard(int index)
        {
            if (index >= hand.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Card c = hand[index];
            if (c.StaminaCost > stamina)
            {
                return false;
            }

            stamina -= c.StaminaCost;
            
            c.Use();
            c.Discard();

            DiscardCard(index);
            return true;
        }
        public void DiscardCard(int index)
        {
            if (index >= hand.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            discard.Add(hand[index]);
            hand.RemoveAt(index);
        }

        public void PrintHand()
        {
            int num = 0;
            hand.ForEach(c =>
            {
                Console.WriteLine($"{num} | {c.Name} : {c.StaminaCost}");
                num++;
            });
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
                health = value;
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
                handSize = value;
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
                stamina = value;
            }
        }
        public int MonsterCount
        {
            get;
            set;
        }
    }
}
