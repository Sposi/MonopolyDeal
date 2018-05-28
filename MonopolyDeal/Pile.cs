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
            CardPile = new List<Card>();
            Owner = player;
            Type = type;
        }

        public int GetSize()
        {
            return CardPile.Count;
        }

        public void AddCard(Card cardToAdd)
        {
            CardPile.Add(cardToAdd);
        }

        public void RemoveCard(Card cardToRemove)
        {
            CardPile.Remove(cardToRemove);
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

        public void ShowCards(Pile pile)
        {
            foreach(Card card in pile.CardPile)
            {
                if ( card != null )
                {
                    Console.WriteLine(card.GetCardDescription());
                }
            }
        }

        public List<Card> CardPile { get; set; }
        public string Owner        { get; set; }
        public string Type         { get; set; }
        public int    Front        { get; set; }
        public int    End          { get; set; }
    }
}
