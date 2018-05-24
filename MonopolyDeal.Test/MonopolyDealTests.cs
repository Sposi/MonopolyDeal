using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MonopolyDeal.Test
{
    [TestClass]
    public class MonopolyDealTests
    {
        [TestMethod]
        public void CheckPropertyCardDescription()
        {
            Card propertyCard = new Card("Property", "Connecticut Ave", 1,
                                         "Light Blue", "");
            Assert.AreEqual(propertyCard.GetCardDescription(),
                            "Light Blue Property card Connecticut Ave, worth 1 million.");
        }

        [TestMethod]
        public void CheckNumCardsAfterDeal()
        {
            CardDeck newDeck = new CardDeck();
            for(int i = 0; i < 10; i++)
            {
                newDeck.DealCard();
            }
            Assert.AreEqual(newDeck.numCards, 96);
        }

        [TestMethod]
        public void CheckShuffleIntegrity()
        {
            CardDeck deck1 = new CardDeck();
            CardDeck deck2 = new CardDeck();
            deck1.ShuffleDeck();

            Assert.AreNotEqual(deck1.DealCard(), deck2.DealCard());
        }

        [TestMethod]
        public void CheckCardCountIntegrity()
        {
            Card card = new Card("", "", 0, "", "");
            Pile property = new Pile("delsuckah", "Property");
            Pile discard = new Pile("game", "Discard");
            CardDeck deck = new CardDeck();
            Pile myHand = new Pile("delsuckahh", "Hand");
            Player player1 = new Player("delsuckahh");

            deck.ShuffleDeck();
            while (card.Type != "Property")
            {
                card = deck.DealCard();
                if (card.Type == "Property")
                {
                    break;
                }
                else
                {
                    discard.addCard(card);
                }
            }
            property.addCard(card);
            player1.DrawCards(2, myHand, deck);

            Assert.AreEqual(106,deck.numCards + discard.getSize() + property.getSize() + myHand.getSize());

        }

    }
}
