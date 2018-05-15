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
            Assert.AreEqual(propertyCard.CardDescription(),
                            "Light Blue Property card Connecticut Ave, worth 1 million.");
        }

        [TestMethod]
        public void CheckIfCardCanGoIntoPropertySet()
        {
            Card propertyCard = new Card("Property", "Boardwalk", 4,
                                         "Blue", "");
            PropertySet blueSet = new PropertySet("Blue", 2);
            Assert.AreEqual(propertyCard.SamePropertySet(propertyCard, blueSet),true);

        }
    }
}
