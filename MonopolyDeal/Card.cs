using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyDeal
{
    public class Card
    {
        public Card(string type, string title, int value,
                    string color, string action)
        {
            Type = type;
            Title = title;
            Value = value;
            Color = color;
            Action = action;
        }

        public string CardDescription()
        {
            string description = $"{Type} card {Title}, worth {Value} million.";
            string colors = "";
            string[] wildColors = Color.Split(colorDelimiter);

            if (Type == "Action")
            {
                description = $"{description} {Action}";
            }
            else if (Type == "Property")
            {
                description = $"{Color} {description}";
            }
            else if (Type == "Wild")
            {
                for (int cardColor = 0; cardColor < wildColors.Length; cardColor++)
                {
                    if (cardColor == wildColors.Length - 1 )
                    {
                        colors = colors + $"{wildColors[cardColor]}";
                    }
                    else
                    {
                        colors = colors + $"{wildColors[cardColor]} or ";
                    }
                }

                description = $"{Type} card, can be {colors}, " + 
                              $"worth {Value} million";
            }

            return description;
        }

        public bool SamePropertySet(Card cardToCheck, PropertySet setToCheckAgainst)
        {
            return cardToCheck.Color == setToCheckAgainst.PropertyColor;
        }


        public string Type   { get; set; }
        public string Title  { get; set; }        
        public int    Value  { get; set; }
        public string Color  { get; set; }
        public string Action { get; set; }

        char[] colorDelimiter = { '|', ',' };
    }
}
