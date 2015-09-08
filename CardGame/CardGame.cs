using System;
using MarcusLonneborg.Cards;

    class CardGame
    {
        static void Main(string[] args)
        {
        Deck deck = new Deck();
        Player[] players = new Player[2];
        players[0] = new Player("Player 1");
        players[1] = new Player("Player 2");
        deck.Shuffle();
        //deck.PrintDeck();
        for (int i = 0; i < 5;i++)
        {
            players[0].addCardToHand(deck.dealCard());
            players[1].addCardToHand(deck.dealCard());

        }
        players[0].printPlayerInfo();
        players[0].printPlayerHand();
        Console.WriteLine();
        players[1].printPlayerInfo();
        players[1].printPlayerHand();
        

        Console.ReadKey();
        }
    }

