using System;
using MarcusLonneborg.Cards;

// Simple Card game project
// by Marcus Lönneborg
// marcuslonneborg@gmail.com

    class CardGame
    {
        static void Main(string[] args)
        {
            // Create new instances of classes
            Deck deck = new Deck();

            Player[] players = new Player[2];
            players[0] = new Player("Player 1");
            players[1] = new Player("Player 2");

            deck.Shuffle(); // Shuffle the deck

        //for (int i = 0; i < 5;i++) // Deal 5 cards to each player
        //{
        //    players[0].addCardToHand(deck.dealCard());
        //    players[1].addCardToHand(deck.dealCard());

        //}
        players[0].addCardToHand(new Card(Suit.Clubs, Value.Queen));
        players[0].addCardToHand(new Card(Suit.Clubs, Value.Queen));
        players[0].addCardToHand(new Card(Suit.Clubs, Value.Queen));
        players[0].addCardToHand(new Card(Suit.Clubs, Value.Queen));
        players[0].addCardToHand(new Card(Suit.Clubs, Value.Ace));
        players[1].addCardToHand(new Card(Suit.Clubs, Value.Queen));
        players[1].addCardToHand(new Card(Suit.Clubs, Value.Queen));
        players[1].addCardToHand(new Card(Suit.Clubs, Value.King));
        players[1].addCardToHand(new Card(Suit.Clubs, Value.Knight));
        players[1].addCardToHand(new Card(Suit.Clubs, Value.King));

        players[0].sortHand();
            players[1].sortHand();



            // Print Test info to console
            players[0].printPlayerInfo();
            players[0].printPlayerHand();
            Console.WriteLine();
            
            players[1].printPlayerInfo();
            players[1].printPlayerHand();

        Console.WriteLine();
            Console.WriteLine(players[0].getPokerHand().pokerHand + " in " + players[0].getPokerHand().rank1 + " and " + players[0].getPokerHand().rank2);
        Console.WriteLine(players[1].getPokerHand().pokerHand + " in " + players[1].getPokerHand().rank1 + " and " + players[1].getPokerHand().rank2);


        Console.ReadKey(); // Wait for keypress
        }
    }

