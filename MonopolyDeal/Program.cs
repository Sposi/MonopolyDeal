using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyDeal
{
    class Program
    {
        static void Main(string[] args)
        {
            CardDeck cards = new CardDeck();
            foreach(Card card in cards.deck)
            {
                Console.WriteLine(card.CardDescription());
            }        
        }
    }
}
