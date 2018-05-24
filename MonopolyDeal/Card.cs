using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyDeal
{
    public class Card
    {
        public Card()
        {
            Type = "Empty";
            Title = "";
            Value = 0;
            Color = "";
            Action = "";
        }

        public Card(string type, string title, int value,
                    string color, string action)
        {
            Type = type;
            Title = title;
            Value = value;
            Color = color;
            Action = action;

            SetAttributes();
        }

        public string GetCardDescription()
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

        public void SetAttributes()
        {
            int[] rent = new int[4];
            switch (this.Color)
            {
                case "Brown":
                case "Teal":
                    rent[0] = 1;
                    rent[1] = 2;
                    TotalProperties = 2;
                    break;
                case "Blue":
                    rent[0] = 3;
                    rent[1] = 8;
                    TotalProperties = 2;
                    break;
                case "Light Blue":
                    rent[0] = 1;
                    rent[1] = 2;
                    rent[2] = 3;
                    TotalProperties = 3;
                    break;
                case "Pink":
                    rent[0] = 1;
                    rent[1] = 2;
                    rent[2] = 4;
                    TotalProperties = 3;
                    break;
                case "Orange":
                    rent[0] = 1;
                    rent[1] = 3;
                    rent[2] = 5;
                    TotalProperties = 3;
                    break;
                case "Red":
                    rent[0] = 2;
                    rent[1] = 3;
                    rent[2] = 6;
                    TotalProperties = 3;
                    break;
                case "Yellow":
                    rent[0] = 2;
                    rent[1] = 4;
                    rent[2] = 6;
                    TotalProperties = 3;
                    break;
                case "Green":
                    rent[0] = 2;
                    rent[1] = 4;
                    rent[2] = 7;
                    TotalProperties = 3;
                    break;
                case "Black":
                    rent[0] = 1;
                    rent[1] = 2;
                    rent[2] = 3;
                    rent[3] = 4;
                    TotalProperties = 4;
                    break;
            }
            Rent = rent;
        }

        public bool VerifyColorMatch(Card currentCard, Card cardToCompare)
        {
            return currentCard.Color == cardToCompare.Color;
        }

        public string Type   { get; set; }
        public string Title  { get; set; }        
        public int    Value  { get; set; }
        public string Color  { get; set; }
        public string Action { get; set; }

        public int    TotalProperties { get; set; }
        public int[]  Rent            { get; set; }

        char[] colorDelimiter = { '|' };
    }
}
