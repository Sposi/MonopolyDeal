using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyDeal
{
    public class PropertySet
    {
        public PropertySet(string propertyColor, int numOfProperties)
        {
            PropertyColor = propertyColor;
            NumOfProperties = numOfProperties;
            Card[] SetOfProperties = new Card[NumOfProperties];
            // call method to put card in array
        }

        // Method to place card in set (array of cards)
        // Method to return rent based on how many cards in set

        public int NumOfProperties;
        public string PropertyColor;
        
    }
}
