using System;
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
                for(int i = 0; i < numOfPlayers; i++)
                {
                    Console.WriteLine($"It is {playerArray[i].Name}'s turn.");
                    Console.WriteLine($"{handArray[i].getSize()} cards in hand.");
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
                    foreach (Card card in hand.CardPile)
                    {
                        if (card != null)
                        {
                            Console.WriteLine(card.GetCardDescription());
                        }
                    }
                    Console.WriteLine("");
                }                
            }
        }

        public void TurnLoop(Player player, int iPlayer)
        {
            // If no cards left in hand, draw 5 cards.  Otherwise draw 2.
            if (handArray[iPlayer].getSize() < 1)
            {
                player.DrawCards(5, handArray[iPlayer], deck);
            }
            else
            {
                player.DrawCards(2, handArray[iPlayer], deck);
            }
            
            Console.WriteLine("You have drawn two cards from the deck.");
            int numOfActions = 3;
            int bankCard = 0;
            bool bankCardSelected = false;
            string response = "";
            do
            {

                Console.WriteLine($"What would you like to do? You have {numOfActions} actions left. (type 'help' for list of commands)");
                response = Console.ReadLine();
                switch(response)
                {
                    case "help":
                        Console.WriteLine("'show hand'         - displays cards currently in your hand\n" +
                                          "'play action'       - displays options for playing action card\n" +
                                          "'lay down property' - lays down a property card\n" +
                                          "'bank'              - displays options for banking money\n" +
                                          "'help'              - displays help menu\n" +
                                          "'show board'        - displays other player's laid down cards\n" +
                                          "'show actions'      - displays the number of actions available to player\n" +
                                          "'nothing'           - skips your action\n" +
                                          "'show bank'         - displays your bank value\n" +
                                          "'hand card count'   - number of cards in hand");
                        break;
                    case "hand card count":
                        Console.WriteLine($"You have {handArray[iPlayer].getSize()} cards in hand.");
                        break;
                    case "show hand":
                        foreach(Card card in handArray[iPlayer].CardPile)
                        {
                            if (card != null)
                            {
                                Console.WriteLine(card.GetCardDescription());
                            }
                        }
                        break;
                    case "play action":
                        numOfActions--;
                        break;
                    case "lay down property":
                        numOfActions--;
                        break;
                    case "bank":
                        // Only create bank here if not exists already
                        bankArray[iPlayer] = new Pile(player.Name, "Bank");
                        Console.WriteLine("What card would you like to bank?");
                        for(int j = 0; j < handArray[iPlayer].getSize() ; j++)
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
                                bankCard = Int32.Parse(Console.ReadLine());
                                bankCardSelected = true;
                            }
                            catch
                            {
                                Console.WriteLine("Oops! Just type the number of the card you'd like to bank.");
                            }
                        } while (bankCardSelected == false);

                        numOfActions--;
                        break;
                    case "show board":
                        break;
                    case "show bank":
                        break;
                    case "show actions":
                        Console.WriteLine($"You have {numOfActions} actions left.");
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
        public Pile[] propertyArray = new Pile[28];
        public string TheWinner;
    }
}
