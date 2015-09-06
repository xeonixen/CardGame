using System;

public enum Suit {Hearts, Spades, Diamons, Clubs }
public enum Value { Two=2,Three, Four, Five, Six, Seven, Eight, Nine, Ten, Knight, Queen, King, Ace}
public class Deck 
{
    private Card[] cards;
    public void Shuffle()
    {
        Random rnd = new Random(); // not good randomization
        for (int i=0;i<cards.Length; i++)
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
        for(int i = 2; i < 15; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                cards[cnt] = new Card((Suit)j, (Value)i);
                cnt++;
            }
        }
        
    }
}

public class Card
{
    private Suit suit;
    private Value value;
    public  Card()
    {

    }
    public Card(Suit _suit, Value _value)
    {
        suit = _suit;
        value = _value;
    }
    public string getCardAsString()
    {
        return value  + " of " + suit;
    }
}

    class Program
    {
        static void Main(string[] args)
        {
        Deck deck = new Deck();
        deck.Shuffle();
        deck.PrintDeck();

        Console.ReadKey();
        }
    }

