using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyDeal
{
    public class Pile
    {
        public Pile(string player, string type)
        {
            CardPile = new Card[106];
            Owner = player;
            Type = type;
            Front = 0;
            End = 0;
        }

        public int getSize()
        {
            return End - Front;
        }

        public void clear()
        {
            Front = 0;
            End = 0;
        }

        public void addCard(Card cardToAdd)
        {
            CardPile[End] = cardToAdd;
            End++;
        }

        public void addCards(Pile pileToAdd)
        {
            while (pileToAdd.getSize() > 0)
            {
                addCard(pileToAdd.nextCard());
            }
        }

        public Card nextCard()
        {
            if (Front == End)
            {
                return null;
            }
            Card cardToReturn = CardPile[Front];
            Front++;
            return cardToReturn;
        }

        public string GetCardsInPile()
        {
            string returnString = "";
            foreach (Card item in CardPile)
            {
                if (item != null)
                {
                    returnString = $"{returnString} \n {item.GetCardDescription()} \n";
                }
                
            }
            return returnString;
        }

        public int GetTotalBankValue()
        {
            int value = 0;
            if (Type != "Bank" )
            {
                return 0;
            }
            
            foreach (Card item in CardPile)
            {
                if (item != null)
                {
                    value = value + item.Value;
                }
                
            }
            return value;
        }


        public Card[] CardPile { get; set; }
        public string Owner    { get; set; }
        public string Type     { get; set; }
        public int    Front    { get; set; }
        public int    End      { get; set; }
    }
}
