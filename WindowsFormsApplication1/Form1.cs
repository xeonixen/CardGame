using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarcusLonneborg.Cards;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Bitmap image = new Bitmap("cards.jpg");     // Cards Bitmap
            Bitmap[] cards = new Bitmap[60];            // Array of Bitmap to hold Card Sprites
            Bitmap test = new Bitmap(1024, 1024);       // Background Bitmap
            Player[] player = new Player[2];            // Player array
            player[0] = new Player("AI");               // Instanciate player 1
            player[1] = new Player("Marcus");           // Instanciate player 2
            Deck deck = new Deck();                     // Create instance of Deck with 52 cards
            deck.Shuffle();                             // Shuffle deck

            // Populate card sprite array
            int cnt = 0;
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 13; i++)
                {

                    cards[cnt++] = new Bitmap(image.Clone(new Rectangle((int)(i * 38.5), (int)(j * 60), 38, 60), image.PixelFormat));
                }
                cnt++;
            }
            // Rearrange cards sprites Aces from 1 to 14
            cards[13] = cards[0];
            cards[27] = cards[14];
            cards[41] = cards[28];
            cards[55] = cards[42];




            // Deal Cards to Players
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    player[j].addCardToHand(deck.dealCard());
                }
                player[j].sortHand();                   // Sort Hand ( Not Working )
            }
            

            
            
            InitializeComponent();

            // Draw both players hands
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    pictureBox1.Image = marcusGraphic.blit(test, cards[(player[j].Hand[i].suit * 14) + player[j].Hand[i].value - 1], 200 + (i * 50), (j*330)+20);
                }
            }
            List<Poker.PokerHandValue> hands = new List<Poker.PokerHandValue>();
            hands.Add(Poker.getPokerHand(player[0].Hand));
            hands.Add(Poker.getPokerHand(player[1].Hand));

            label1.Text = Enum.GetName(typeof(PokerHand), hands[0].pokerHand); 
            label2.Text = Enum.GetName(typeof(PokerHand), hands[1].pokerHand);
            int result = Poker.ComparePokerHand(hands);
            if (result == 0) label1.Text += " Winner";
            else label2.Text += " Winner";
        }

        /// <summary>
        /// Method to draw small bitmap(sprite) on large bitmap(background)
        /// </summary>
        public class marcusGraphic
        {
            public static Bitmap blit(Bitmap largeBitmap, Bitmap smallBitmap, int x, int y)
            {
                Graphics g = Graphics.FromImage(largeBitmap);
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                smallBitmap.MakeTransparent();
                g.DrawImage(smallBitmap, new Point(x, y));
                return largeBitmap;
            }

        }
    }
}
 