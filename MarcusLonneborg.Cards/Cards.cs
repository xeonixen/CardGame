using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcusLonneborg.Cards
{

    public enum Suit { Hearts, Spades, Diamons, Clubs }
    public enum Value { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Knight, Queen, King, Ace }
    public enum PokerHand {None, OnePair,TwoPair, ThreeOfaKind,Straight,Flush,FullHouse, FourOfaKind, StraightFlush,RoyalFlush }
    public class PokerHandValue
    {
        public Value rank1, rank2;
        public PokerHand pokerHand;
    }
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
        private string name;
        private int id;
        private static int numberOfPlayers = 0;
        private int numberOfCards = 0;
        private Card[] hand;

        public Player()
        {
            name = "Default";
            id = numberOfPlayers++;
            hand = new Card[5];
        }

        public Player(string _name)
        {
            name = _name;
            id = numberOfPlayers++;
            hand = new Card[5];
        }

        public void addCardToHand(Card _card)
        {
            hand[numberOfCards++] = _card;
        }
        public void sortHand()
        {
            Array.Sort(hand, delegate (Card card1, Card card2) // Sort by value
            {
                return card1.value.CompareTo(card2.value);
            });
            Array.Sort(hand, delegate (Card card1, Card card2) // sort by suit
             {
                 return card1.suit.CompareTo(card2.suit);
             });

        }
        public PokerHandValue getPokerHand() 
        {
            // record how many of each rank we have
            int[] rank = new int[15];
            PokerHandValue _pokerHandValue = new PokerHandValue();
            _pokerHandValue.pokerHand = PokerHand.None;
            _pokerHandValue.rank1 = Value.Two;
            _pokerHandValue.rank2 = Value.Two;

            for(int i=0;i<hand.Length;i++)
            {
                rank[hand[i].value]++;
            }
            int sameCards = 1, sameCards2 = 1;
            //int firstValue = 0, secondValue = 0;
            for(int i = 2; i < 15; i++)
            {
                if(rank[i]> sameCards)
                {
                    if(sameCards!=1)
                    {
                        sameCards2 = sameCards;
                        _pokerHandValue.rank2 = _pokerHandValue.rank1;
                    }
                    sameCards = rank[i];
                    _pokerHandValue.rank1 = (Value)i;
                }
                else if(rank[i]>sameCards2)
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

            return _pokerHandValue;
        }

        public void printPlayerInfo()
        {
            Console.WriteLine("Name: " + name + "\n" +
                              "ID: " + id);
        }
        public void printPlayerHand()
        {
            foreach(Card c in hand)
            {
                Console.WriteLine(c.getCardAsString());
            }
        }

    }

}
