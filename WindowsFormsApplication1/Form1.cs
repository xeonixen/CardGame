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
            Bitmap image = new Bitmap("cards.jpg");
            Bitmap[] cards = new Bitmap[60];
            Bitmap test = new Bitmap(1024, 1024);
            Player player = new Player("Marcus");
            Deck deck = new Deck();
            deck.Shuffle();
           for(int i = 0;i<5;i++)
           {
                player.addCardToHand(deck.dealCard());
                
            }

            player.sortHand();


            int cnt = 0;
            for(int j = 0; j < 4; j++)
            {
                for(int i = 0; i < 13; i++)
                {
                    
                     cards[cnt++] = new Bitmap(image.Clone(new Rectangle((int)(i*38.5), (int)(j*60), 38, 60), image.PixelFormat));
                    //pictureBox1.Image = cards[cnt-1];
                }
                cnt++;
            }
            cards[13] = cards[0];
            cards[27] = cards[14];
            cards[41] = cards[28];
            cards[55] = cards[42];
            
            InitializeComponent();

            //test = marcusGraphic.blit(test, cards[1], 100, 100);
            for(int i = 0; i < 5; i++)
            {
                pictureBox1.Image = marcusGraphic.blit(test, cards[(player.Hand[i].suit * 14) + player.Hand[ i].value-1 ], 200+(i*50), 350);
            }
            label1.Text = Enum.GetName(typeof(PokerHand), Poker.getPokerHand(player.Hand).pokerHand); //Poker.ph(handValue);
            
            //pictureBox1.Image = blitSurface;

        }
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
 