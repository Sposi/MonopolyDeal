using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyDeal
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
            playerHand = new Pile(Name, "Hand");
        }

        public void DrawCards(int numOfCards, Pile playerHand, CardDeck deck)
        {
            Card temp = new Card();

            for (int i = 0; i < numOfCards; i++)
            {
                temp = deck.DealCard();
                playerHand.addCard(temp);
            }
        }       

        // method for playing action card

        // method for paying player

        public string Name { get; set; }
        public Pile playerHand;
    }
}
