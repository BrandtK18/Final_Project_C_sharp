using System.Reflection.Metadata.Ecma335;

namespace Game
{
    public class Player : IDamageable
    {
        private List<Card> cards;
        private List<Card> discard;
        private List<Card> hand;

        private Random rand = new Random();

        private int healthMax;
        private int health;

        private int staminaMax;
        private int stamina;

        private int handSize; // same size as hand list

        public Player() : this(10, 3, 3, 0)
        {

        }
        public Player(int healthMax, int staminaMax, int handSize, int monsterCount)
        {
            HealthMax = healthMax;
            Health = healthMax;
            StaminaMax = staminaMax;
            Stamina = staminaMax;
            HandSize = handSize;
            MonsterCount = monsterCount;

            cards = new List<Card>();
            discard = new List<Card>();
            hand = new List<Card>();
        }

        #region Card Management Methods
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
        
        #endregion

        #region Gameplay Methods
        public bool PlayCard(int index, out string logString)
        {
            if (index >= hand.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Card c = hand[index];
            if (c.StaminaCost > stamina)
            {
                logString = "";
                return false;
            }

            stamina -= c.StaminaCost;

            if (c is WeaponCard a)
            {
                if (CurrentMonster != null)
                    a.SendAttack += CurrentMonster.ReceiveAttack;

                logString = $"You did {a.Damage} damage with your {a.Name}";
            }
            else if (c is ItemCard i)
            {
                Health += i.Health;
                Stamina += i.Stamina;
                logString = $"You used a {i.Name} to gain {i.Health} health and {i.Stamina} stamina";
            }
            else
            {
                logString = "";
            }

            c.Use();
            

            DiscardCard(index);
            return true;
        }
        public void DiscardCard(int index)
        {
            if (index >= hand.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (hand[index] is IAttack a)
            {
                if (CurrentMonster != null)
                    a.SendAttack -= CurrentMonster.ReceiveAttack;
            }

            hand[index].Discard();

            discard.Add(hand[index]);
            hand.RemoveAt(index);
        }
        public void EndTurn()
        {
            while(hand.Count > 0)
            {
                DiscardCard(0);
            }
            Stamina = StaminaMax;
            Reshuffle();
            DrawHand();
        }

        // IDamagable
        public void ReceiveAttack(object sender, EventArgs e)
        {
            if (e is AttackArgs a)
                TakeDamage(a.Damage);
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        #endregion

        #region Display Methods
        public void PrintHand()
        {
            Console.WriteLine("- Hand ----");

            int num = 0;
            hand.ForEach(c =>
            {
                Console.WriteLine($"{num} | {c.Name} : {c.StaminaCost}");
                num++;
            });
        }
        public void PrintStats()
        {
            Console.WriteLine($"HP: {health}/{healthMax} | Stamina: {stamina}/{staminaMax}");
        }
        
        #endregion

        public int HealthMax
        {
            get => healthMax;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Max Health cannot be negative");
                }
                healthMax = value;
            }
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
                if (health > healthMax)
                {
                    health = healthMax;
                }
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
        public int StaminaMax
        {
            get => staminaMax;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Max Stamina cannot be negative");
                }
                staminaMax = value;
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
                if (stamina > staminaMax)
                {
                    stamina = staminaMax;
                }
            }
        }
        public int MonsterCount
        {
            get;
            set;
        }
        public Monster CurrentMonster { get; set; }
    }
}
