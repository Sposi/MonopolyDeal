﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonopolyDeal
{
    public class Game
    {
        public Game()
        {            
        }

        public void play()
        {
            Welcome();
            deck.ShuffleDeck();
            SetupPlayers();
            DealPlayersInitialHand();
            do
            {
                for (int i = 0; i < numOfPlayers; i++)
                {
                    Console.WriteLine($"It is {playerArray[i].Name}'s turn.");
                    TurnLoop(playerArray[i], i);

                }
            } while (TheWinner == null);
        }

        public void Welcome()
        {
            Console.WriteLine("Welcome to Monopoly Deal!");
            Thread.Sleep(500);
            Console.WriteLine("Let's get your game setup.");
            for(int i = 0; i < 5; i++)
            {
                Console.Write(". ");
                Thread.Sleep(250);
            }
            Console.WriteLine();
        }

        public void SetupPlayers()
        {
            string countRemind = "Please enter an integer between 2 and 5.";
            string[] players = new string[5];

            do
            {
                Console.WriteLine("How many players?  (2-5 players)");
                try
                {
                    numOfPlayers = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"{e.Message} \n{countRemind}");
                }
                if (numOfPlayers < 2 && numOfPlayers != 0)
                {
                    Console.WriteLine($"You can't play by yourself...\n{countRemind}");
                }
                if (numOfPlayers > 5)
                {
                    Console.WriteLine($"I can't keep track of that many players!\n{countRemind}");
                }

            } while (numOfPlayers < 2 || numOfPlayers > 5); 
            
            for (int i = 0; i < numOfPlayers; i++)
            {
                Console.WriteLine($"Please enter name for player {i + 1}");
                players[i] = Console.ReadLine();
                playerArray[i] = new Player(players[i]);
                handArray[i] = new Pile(playerArray[i].Name, "Hand");
                bankArray[i] = new Pile(playerArray[i].Name, "Bank");
                propertyArray[i] = new List<PropertySet>();
            }
        }

        public void DealPlayersInitialHand()
        {
            foreach (Player player in playerArray)
            {
                if (player != null)
                {
                    foreach(Pile hand in handArray)
                    {
                        if(hand != null && hand.Owner == player.Name)
                        {
                            player.DrawCards(5, hand, deck);
                        }
                    }
                }
            }
        }

        public void ShowAllPlayerHands()
        {
            foreach (Pile hand in handArray)
            {
                if (hand != null)
                {
                    Console.WriteLine($"All cards in {hand.Owner}'s hand:");
                    hand.ShowCards();
                    Console.WriteLine("");
                }                
            }
        }

        public void TurnLoop(Player player, int iPlayer)
        {
            int numOfActions = 3;
            int selectedCard = 0;
            bool cardSelected = false;
            string response = "";

            // If no cards left in hand, draw 5 cards.  Otherwise draw 2.
            if (handArray[iPlayer].GetSize() < 1)
            {
                player.DrawCards(5, handArray[iPlayer], deck);
            }
            else
            {
                player.DrawCards(2, handArray[iPlayer], deck);
            }
            
            Console.WriteLine("You have drawn two cards from the deck.");            
            do
            {

                Console.WriteLine($"What would you like to do? You have {numOfActions} actions left. (type 'help' for list of commands)");
                response = Console.ReadLine();
                switch(response)
                {
                    case "help":
                        Console.WriteLine("'bank'              - displays options for banking money\n" +
                                          "'lay down property' - displays options for laying down a property card\n" +
                                          "'play action'       - displays options for playing action card\n" +
                                          "'show hand'         - displays cards currently in your hand\n" +
                                          "'show board'        - displays other player's laid down cards\n" +     
                                          "'show bank'         - displays cards in bank\n" +
                                          "'show bank value'   - displays your bank value\n" +
                                          "'show properties'   - displays your properties\n" +
                                          "'show actions'      - displays the number of actions available to player\n" +
                                          "'hand count'        - number of cards in hand\n" +
                                          "'bank count'        - number of cards in bank\n" +                                                                               
                                          "'nothing'           - skips your action\n" +
                                          "'help'              - displays help menu\n" +
                                          "''");
                        break;
                    case "bank":
                        Console.WriteLine("What card would you like to bank?");
                        for (int j = 0; j < handArray[iPlayer].GetSize(); j++)
                        {
                            if (handArray[iPlayer].CardPile[j] != null &&
                                handArray[iPlayer].CardPile[j].Type == "Action" ||
                                handArray[iPlayer].CardPile[j].Type == "Cash" ||
                                handArray[iPlayer].CardPile[j].Type == "")
                            {
                                Console.WriteLine($"{j} {handArray[iPlayer].CardPile[j].GetCardDescription()}");
                            }
                        }
                        do
                        {
                            try
                            {
                                selectedCard = Int32.Parse(Console.ReadLine());
                                cardSelected = true;
                            }
                            catch
                            {
                                Console.WriteLine("Oops! Just type the number of the card you'd like to bank.");
                            }
                        } while (cardSelected == false);
                        // Put selected card in bank
                        bankArray[iPlayer].AddCard(handArray[iPlayer].CardPile[selectedCard]);
                        // and remove from hand
                        handArray[iPlayer].RemoveCard(handArray[iPlayer].CardPile[selectedCard]);
                        numOfActions--;
                        // reset variables
                        selectedCard = 0;
                        cardSelected = false;
                        break;
                    case "lay down property":
                        Console.WriteLine("What property would you like to lay down?");

                        var queryPropertyCards = from card in handArray[iPlayer].CardPile
                                                 where card.Type == "Property" ||
                                                       card.Type == "Wild" ||
                                                       card.Type == ""
                                                 select card;

                        for (int j = 0; j < queryPropertyCards.ToArray().Count(); j++)
                        {
                            Console.WriteLine($"{j} - {queryPropertyCards.ToArray()[j].GetCardDescription()}");
                        }

                        do
                        {
                            try
                            {
                                selectedCard = Int32.Parse(Console.ReadLine());
                                cardSelected = true;
                            }
                            catch
                            {
                                Console.WriteLine("Oops! Just type the number of the card you'd like to bank.");
                            }

                        } while (cardSelected == false);

                        Console.WriteLine($"You selected {queryPropertyCards.ToArray()[selectedCard].GetCardDescription()}");

                        var queryHandForProperty = from   handCard in handArray[iPlayer].CardPile
                                                   where  handCard == queryPropertyCards.ToArray()[selectedCard]
                                                   select handCard;                            

                        // query should only return one record, so grab first record
                        // Console.WriteLine($"You selected {queryHandForProperty.ToArray()[0].GetCardDescription()}");

                        // query for any properties of same color and not full
                        bool addedToExisting = false;
                        for(int i = 0; i < propertyArray[iPlayer].Count(); i++)
                        {
                            var propertyQuery = from   propertySet in propertyArray[iPlayer][i].CardPile
                                                where  propertyArray[iPlayer][i].Color    == queryHandForProperty.ToArray()[0].Color &&
                                                       propertyArray[iPlayer][i].GetSize() < propertyArray[iPlayer][i].Capacity
                                                select propertySet;
                            // if avail, add to existing
                            if (propertyQuery.Count() > 0 )
                            {
                                propertyArray[iPlayer][i].AddCard(queryPropertyCards.ToArray()[selectedCard]);
                                addedToExisting = true;
                                break;
                            }
                        }

                        // if  not avail, create new property pile
                        if (!addedToExisting)
                        {
                            // create property set
                            propertyArray[iPlayer].Add(new PropertySet(playerArray[iPlayer].Name,"Property",queryHandForProperty.ToArray()[0].Color));
                            // add to property set
                            propertyArray[iPlayer].Last().AddCard(queryPropertyCards.ToArray()[selectedCard]);
                            // remove from hand
                            handArray[iPlayer].RemoveCard(queryPropertyCards.ToArray()[selectedCard]);
                        }
                                                
                        numOfActions--;

                        // reset variables
                        selectedCard = 0;
                        cardSelected = false;
                        addedToExisting = false;
                        break;
                    case "play action":
                        numOfActions--;
                        break;
                    case "show hand":
                        handArray[iPlayer].ShowCards();
                        break;
                    case "show board":
                        break;
                    case "show bank":
                        bankArray[iPlayer].ShowCards();
                        break;
                    case "show bank value":
                        Console.WriteLine($"You have {bankArray[iPlayer].GetTotalValue()} in the bank.");
                        break;
                    case "show properties":
                        foreach(PropertySet property in propertyArray[iPlayer])
                        {
                            Console.WriteLine($"Cards in {property.Color} set:\n");
                            foreach(Card card in property.CardPile)
                            {
                                Console.WriteLine(card.GetCardDescription());
                            }
                        }
                        break;
                    case "show actions":
                        Console.WriteLine($"You have {numOfActions} actions left.");
                        break;
                    case "hand count":
                        Console.WriteLine($"You have {handArray[iPlayer].GetSize()} card(s) in your hand.");
                        break;
                    case "bank count":
                        Console.WriteLine($"You have {bankArray[iPlayer].GetSize()} card(s) in your bank.");
                        break;                                                                                                                       
                    case "nothing":
                        numOfActions--;
                        break;
                    default:
                        Console.WriteLine("Sorry, that is not a valid action.  Try again.");
                        break;                   
                }                                        
            } while (numOfActions > 0);
        }

        CardDeck deck = new CardDeck();
        public int numOfPlayers = 0;

        public Player[] playerArray = new Player[5];
        public Pile[] handArray     = new Pile[5];
        public Pile[] bankArray     = new Pile[5];

        List<PropertySet>[] propertyArray = new List<PropertySet>[5];
        public string TheWinner;
    }
}
