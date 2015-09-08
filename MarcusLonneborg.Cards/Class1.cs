using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcusLonneborg.Cards
{

    public enum Suit { Hearts, Spades, Diamons, Clubs }
    public enum Value { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Knight, Queen, King, Ace }


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
        private Suit suit;
        private Value value;
        public Card()
        {
            throw new Exception("Must initialize with values, not using default constructor.");
           // suit = Suit.Hearts;
           // value = Value.Ace;
        }
        public Card(Suit _suit, Value _value)
        {
            suit = _suit;
            value = _value;
        }
        
        public string getCardAsString()
        {
            return value + " of " + suit;
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
