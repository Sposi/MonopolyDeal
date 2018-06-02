using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace MonopolyDeal
{
    public class CardDeck
    {
        public CardDeck()
        {
            deck = new Card[106];
            PopulateDeck();
        }

        private void PopulateDeck()
        {
            string configPath = @"MonopolyDealConfig.csv";
            int index = 0;                    

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
                    int     card_id = Int32.Parse(fields[5]);

                    deck[index] = new Card(type, title, value, color, action, card_id);
                    index++;
                }
            }

            numCards = 106;
        }

        public void ShuffleDeck()
        {
            Random randGenerator = new Random();            

            for (int nextCard = 0; nextCard < numCards - 1; nextCard++)
            {
                int randNum = randGenerator.Next(nextCard, numCards);
                Card temp = deck[nextCard];
                deck[nextCard] = deck[randNum];
                deck[randNum] = temp;
            }
        }

        public Card DealCard()
        {
            if (numCards == 0) { return null; }
            numCards--;
            return deck[numCards];
        }

        public Card[] deck;
        public int numCards { get; set; }
    }
}
