using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MarcusLonneborg.Cards
{

    public enum Suit { Clubs, Diamonds, Hearts, Spades }
    public enum Value { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Knight, Queen, King, Ace }
    public enum PokerHand { None, OnePair, TwoPair, ThreeOfaKind, Straight, Flush, FullHouse, FourOfaKind, StraightFlush, RoyalFlush }


    public class Deck
    {
        private Card[] cards;
        private int numberOfCards = 0;

        public void Shuffle()
        {
            Random rnd = new Random(); // not good randomization
            for (int i = 0; i < cards.Length; i++)
            {
                Card tmp = cards[i];
                int r = rnd.Next(cards.Length);
                cards[i] = cards[r];
                cards[r] = tmp;
            }

        }
        public void PrintDeck()
        {
            foreach (Card c in cards)
            {
                Console.WriteLine(c.getCardAsString());
            }
        }
        public Deck()
        {
            int cnt = 0;
            cards = new Card[52];
            for (int i = 2; i < 15; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    cards[cnt] = new Card((Suit)j, (Value)i);
                    cnt++;
                }
            }
            numberOfCards = 51;

        }
        public Card dealCard()
        {

            return cards[numberOfCards--];
        }
    }

    public class Card
    {
        private Suit _suit;
        private Value _value;
       
        public Card()
        {
            throw new Exception("Must initialize with values, not using default constructor.");
           // suit = Suit.Hearts;
           // value = Value.Ace;
        }
        public Card(Suit _tsuit, Value _tvalue)
        {
            _suit = _tsuit;
            _value = _tvalue;
        }
        
        public string getCardAsString()
        {
            return _value + " of " + _suit;
        }
        public int suit
        {
            get
            {
                return (int)_suit;
            }
        }
        public int value
        {
            get
            {
                return (int)_value;
            }
        }

    }

    public class Player
    {
        public string Name
        {
            get;
        }
        private int id;
        private static int numberOfPlayers = 0;
        private int numberOfCards = 0;
        //private Card[] Hand;

        // Get Set
        public Card[] Hand
        {
            get;
        }

        // Constructors

        public Player()
        {
            Name = "Default";
            id = numberOfPlayers++;
            Hand = new Card[5];
        }

        public Player(string _name)
        {
            Name = _name;
            id = numberOfPlayers++;
            Hand = new Card[5];
        }




        // Methods
        public void addCardToHand(Card _card)
        {
            Hand[numberOfCards++] = _card;
        }
        public void sortHand()
        {
            Array.Sort(Hand, delegate (Card card1, Card card2) // Sort by value
            {
                return card1.value.CompareTo(card2.value);
            });
            Array.Sort(Hand, delegate (Card card1, Card card2) // sort by suit
             {
                 return card1.suit.CompareTo(card2.suit);
             });

        }
        

        
        public void printPlayerInfo()
        {
            Console.WriteLine("Name: " + Name + "\n" +
                              "ID: " + id);
        }
        public void printPlayerHand()
        {
            foreach(Card c in Hand)
            {
                Console.WriteLine(c.getCardAsString());
            }
        }

    }


    public class Poker
    {
        public class PokerHandValue
        {
            public Value rank1, rank2;
            public PokerHand pokerHand;
        }


        public static int ComparePokerHand(Card[]  hand0, Card[] hand1)
        {
            if (Poker.getPokerHand(hand0).pokerHand > Poker.getPokerHand(hand1).pokerHand)
                return 0;
            else if (Poker.getPokerHand(hand0).pokerHand == Poker.getPokerHand(hand1).pokerHand && Poker.getPokerHand(hand0).rank1 > Poker.getPokerHand(hand1).rank1)
                return 0;
            else 
                return 1;

        }


        public static PokerHandValue getPokerHand(Card[] hand)
        {
            // record how many of each rank we have
            int[] rank = new int[15];
            PokerHandValue _pokerHandValue = new PokerHandValue();
            _pokerHandValue.pokerHand = PokerHand.None;
            _pokerHandValue.rank1 = Value.Two;
            _pokerHandValue.rank2 = Value.Two;

            for (int i = 0; i < hand.Length; i++)
            {
                rank[hand[i].value]++;
            }
            int sameCards = 1, sameCards2 = 1;
            //int firstValue = 0, secondValue = 0;
            for (int i = 2; i < 15; i++)
            {
                if (rank[i] > sameCards)
                {
                    if (sameCards != 1)
                    {
                        sameCards2 = sameCards;
                        _pokerHandValue.rank2 = _pokerHandValue.rank1;
                    }
                    sameCards = rank[i];
                    _pokerHandValue.rank1 = (Value)i;
                }
                else if (rank[i] > sameCards2)
                {
                    sameCards2 = rank[i];
                    _pokerHandValue.rank2 = (Value)i;
                }
            }

            if (sameCards2 == 1)
            {
                if (sameCards == 2)
                    _pokerHandValue.pokerHand = PokerHand.OnePair;
                else if (sameCards == 3)
                    _pokerHandValue.pokerHand = PokerHand.ThreeOfaKind;
                else if (sameCards == 4)
                    _pokerHandValue.pokerHand = PokerHand.FourOfaKind;

            }
            else if (sameCards2 == 2)
            {
                if (sameCards == 2)
                    _pokerHandValue.pokerHand = PokerHand.TwoPair;
                else if (sameCards == 3)
                    _pokerHandValue.pokerHand = PokerHand.FullHouse;



            }
            else if (sameCards2 == 3)
            {
                if (sameCards == 2)
                    _pokerHandValue.pokerHand = PokerHand.FullHouse;
            }

            if (sameCards == 1)
            {
                bool straight = true;
                bool flush = true;
                if (hand[0].value <= 10)
                {
                    for (int i = 0; i < hand.Length - 1; i++)
                    {
                        if (hand[i].value != hand[i + 1].value - 1)
                            straight = false;
                       
                    }
                }
                else straight = false;

                for (int i = 0; i < hand.Length-1; i++)
                {
                    if (hand[i].suit != hand[i + 1].suit)
                        flush = false;
                }

                if (straight && flush)
                {
                    if (hand[0].value == 10 && hand[0].suit == (int)Suit.Spades)
                        _pokerHandValue.pokerHand = PokerHand.RoyalFlush;
                    else
                    {
                        _pokerHandValue.pokerHand = PokerHand.StraightFlush;
                        _pokerHandValue.rank1 = (Value)hand[0].value;
                        _pokerHandValue.rank2 = (Value)hand[4].value;

                    }
                }
                if (straight && !flush)
                {
                    _pokerHandValue.pokerHand = PokerHand.Straight;
                    _pokerHandValue.rank1 = (Value)hand[0].value;
                    _pokerHandValue.rank2 = (Value)hand[4].value;
                }
                if (!straight && flush)
                {
                    _pokerHandValue.pokerHand = PokerHand.Flush;
                    _pokerHandValue.rank1 = (Value)hand[0].value;
                    _pokerHandValue.rank2 = (Value)hand[4].value;
                }

            }

            return _pokerHandValue;
        }
        public static string ph(Poker.PokerHandValue epa)
        {
            return Convert.ToString(epa);
        }
    }




}

