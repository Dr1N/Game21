using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    class Game
    {
        //======================= Fields =======================

        private Deck deck;
        private Deck playerCards;
        private Deck computerCards;
        private int money;
        private int bet;
        private bool first, endGame, endPlayerGame;
        private int playerWins, computerWins;
        private enum GAME_RESULT
        {
            NEXT = 0,
            PLAYER_WIN,
            COMPUTER_WIN,
            DRAW
        };

        //======================= Methods =======================

        public Game()
        {
            deck = new Deck(true);
            deck.shuffleDeck();
            playerCards = new Deck();
            computerCards = new Deck();
            money = GameSettings.startMoney;
            bet = GameSettings.defaultBet;
            endGame = false;
            endPlayerGame = false;
            first = true;
            playerWins = 0;
            computerWins = 0;
        }

        /// <summary>
        /// Begin game
        /// </summary>
        public void play()
        {
            int select = 0;
            while (true)
            {
                beginGame();
                displayGame();

                while (!endGame && money >= bet)
                {
                    select = showMenu(GameSettings.gameMenu);
                    switch (select)
                    {
                        case 1:
                            playerCards.pushCard(deck.popCard());
                            displayGame();
                            if (playerCards.getPoints() > 21)
                            {
                                goto case 2;
                            }
                            break;
                        case 2:
                            endPlayerGame = true;
                            computerGame();
                            displayGame();
                            break;
                        default:
                            Console.WriteLine("Ошибка. Игровое меню.");
                            break;
                    }
                }

                //Check money

                if (money <= 0 || bet > money)
                {
                    Console.WriteLine("\n\nGame Over");
                    return;
                }

                //Next game?

                select = showMenu(GameSettings.mainMenu);
                switch (select)
                {
                    case 1:
                        beginGame();
                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("Ошибка. Главное меню.");
                        break;
                }
            }
        }

        /// <summary>
        /// Game begins - deal two cards
        /// </summary>
        private void beginGame()
        {
            endGame = false;
            endPlayerGame = false;
            first = true;
            deck.fillDeck();
            deck.shuffleDeck();
            playerCards.clearDeck();
            computerCards.clearDeck();

            Card tempCard;

            tempCard = deck.popCard();
            playerCards.pushCard(tempCard);
            tempCard = deck.popCard();
            playerCards.pushCard(tempCard);

            tempCard = deck.popCard();
            computerCards.pushCard(tempCard);
            tempCard = deck.popCard();
            computerCards.pushCard(tempCard);
            computerCards.setCardState(Card.STATE.CLOSE);

            first = false;
        }

        /// <summary>
        /// Computer game
        /// </summary>
        private void computerGame()
        {
            int computerPoints = computerCards.getPoints();
            int playerPoints = playerCards.getPoints();
            if (playerPoints > 21)
            {
                return;
            }
            while (computerPoints <= GameSettings.maxComputerPoints && computerPoints < playerPoints)
            {
                computerCards.pushCard(deck.popCard());
                computerPoints = computerCards.getPoints();
            }
        }

        /// <summary>
        /// Game analysis
        /// </summary>
        /// <returns>Game state</returns>
        private GAME_RESULT gameAnalysis()
        {
            int playerPoints = playerCards.getPoints();
            int computerPoints = computerCards.getPoints();
            GAME_RESULT result = GAME_RESULT.NEXT;

            if (first)
            {
                if (playerPoints > 21 && computerPoints > 21)
                {
                    beginGame();
                }
                else if (playerPoints > 21 || computerPoints == 21)
                {
                    result = GAME_RESULT.COMPUTER_WIN;
                }
                else if (playerPoints == 21 || computerPoints > 21)
                {
                    result = GAME_RESULT.PLAYER_WIN;
                }
                else if (playerPoints == 21 && computerPoints == 21)
                {
                    result = GAME_RESULT.DRAW;
                }
            }

            else if (endPlayerGame)
            {
                if (playerPoints > 21 || computerPoints == 21 || (computerPoints > playerPoints && computerPoints <= 21))
                {
                    result = GAME_RESULT.COMPUTER_WIN;
                }
                else if (computerPoints > 21 || playerPoints > computerPoints)
                {
                    result = GAME_RESULT.PLAYER_WIN;
                }
                else if (computerPoints == playerPoints)
                {
                    result = GAME_RESULT.DRAW;
                }
            }
            return result;
        }

        /// <summary>
        /// Show game state
        /// </summary>
        private void displayGame()
        {
            Console.Clear();
            if (endPlayerGame)
            {
                computerCards.setCardState(Card.STATE.OPEN);
            }
            computerCards.drawDeck(GameSettings.xOrigin, GameSettings.yOrigin, GameSettings.cardOffset);
            playerCards.drawDeck(GameSettings.xOrigin, GameSettings.yOrigin + GameSettings.cardDistance, GameSettings.cardOffset);

            GAME_RESULT result = gameAnalysis();
            switch (result)
            {
                case GAME_RESULT.NEXT:
                    printPointsMoneyBetScore();
                    break;
                case GAME_RESULT.PLAYER_WIN:
                    playerWins++;
                    money += bet * GameSettings.prizeFactor;
                    endGame = true;
                    printPointsMoneyBetScore();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nВы выиграли");
                    Console.ResetColor();
                    break;
                case GAME_RESULT.COMPUTER_WIN:
                    computerWins++;
                    money -= bet;
                    endGame = true;
                    printPointsMoneyBetScore();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nКомпьютер выиграл");
                    Console.ResetColor();
                    break;
                case GAME_RESULT.DRAW:
                    endGame = true;
                    printPointsMoneyBetScore();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nНичья");
                    Console.ResetColor();
                    break;
                default:
                    break;
            }
            Console.ResetColor();
        }

        private void printPointsMoneyBetScore()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nИгрок:\t{0}", playerCards.getPoints());
            Console.WriteLine("Деньги:\t{0}", money);
            Console.WriteLine("Ставка:\t{0}", bet);
            Console.WriteLine("Счёт:\t{0} : {1}", playerWins, computerWins);
            if (endPlayerGame)
            {
                Console.WriteLine("\nКомп:\t{0}", computerCards.getPoints());
            }
        }

        /// <summary>
        /// Show menu
        /// </summary>
        /// <param name="menu">string array with menu items</param>
        /// <returns>selected menu item</returns>
        private int showMenu(string[] menu)
        {
            int activeIndex = 1;
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            while (true)
            {
                int index = 1;
                Console.CursorTop = cursorTop;
                Console.CursorLeft = cursorLeft;
                Console.WriteLine();
                foreach (string menuItem in menu)
                {
                    if (index == activeIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("{0}. {1}", index, menuItem);
                    index++;
                    Console.ResetColor();
                }
                ConsoleKeyInfo userKey = Console.ReadKey(true);
                switch (userKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        activeIndex++;
                        if (activeIndex > menu.Length)
                        {
                            activeIndex = 1;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        activeIndex--;
                        if (activeIndex <= 0)
                        {
                            activeIndex = menu.Length;
                        }
                        break;
                    case ConsoleKey.Enter:
                        return activeIndex;
                    default:
                        break;
                }
            }
        }
    }
}
