using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace MonopolyDeal
{
    class CardDeck
    {
        public CardDeck()
        {
            deck = new Card[106];
            populateDeck();
        }

        private void populateDeck()
        {
            string configPath = @"MonopolyDealConfig.csv";
            int index = 0;                    

            // Read in csv and create card
            using (TextFieldParser csvParser = new TextFieldParser(configPath))
            {
                csvParser.SetDelimiters(new string[] { "," });

                // Skip column titles
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    string     type = fields[0];
                    string    title = fields[1];
                    int       value = Int32.Parse(fields[2]);
                    string    color = fields[3];
                    string   action = fields[4];

                    deck[index] = new Card(type, title, value, color, action);
                    index++;
                }
            }

            numCards = 106;
        }

        //method to shuffle deck

        //method to deal card

        public Card[] deck;
        public int numCards { get; set; }
    }
}
