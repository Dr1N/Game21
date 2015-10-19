using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game21
{
    class Deck
    {
        //======================= Fields =======================

        private int amount;
        public ArrayList cards;

        //======================= Методы =======================

        public Deck()
        {
            cards = new ArrayList();
            setAmount();
        }

        /// <summary>
        /// Create deck
        /// </summary>
        /// <param name="filled">true - filles; false - empty</param>
        public Deck(bool filled)
        {
            cards = new ArrayList();
            setAmount();
            if (filled)
            {
                fillDeck();
            }
        }

        private void setAmount()
        {
            if (CardData.face.Length != CardData.point.Length)
            {
                Console.WriteLine("Ошибка. Невозможно создать колоду. Неверные данные.");
                return;
            }
            int faceCount = CardData.face.Length;
            int suitCount = CardData.suit.Length;
            amount = faceCount * suitCount;
        }

        public void fillDeck()
        {
            string currentFace, currentSuit;
            Card currentCard;
            cards.Clear();
            int faceCardCount = CardData.face.Length;
            for (int i = 0; i < amount; i++)
            {
                currentFace = CardData.face[i % faceCardCount];
                currentSuit = CardData.suit[i / faceCardCount];
                currentCard = new Card(currentFace, currentSuit);
                cards.Add(currentCard);
            }
        }

        public void shuffleDeck()
        {
            if (cards.Count == amount && amount != 0)
            {
                Random rand = new Random();
                int sourceIndex, destIndex;
                Card tmpCard;
                for (sourceIndex = 0; sourceIndex < amount; sourceIndex++)
                {
                    destIndex = rand.Next(0, amount);
                    tmpCard = (Card)cards[sourceIndex];
                    cards[sourceIndex] = cards[destIndex];
                    cards[destIndex] = tmpCard;
                }
            }
            else
            {
                Console.WriteLine("Ошибка. Данную колоду нельзя перетасовать.");
            }
        }

        /// <summary>
        /// Get first card in deck
        /// </summary>
        /// <returns>Card</returns>
        public Card popCard()
        {
            Card firstCard = null;
            if (cards.Count != 0)
            {
                firstCard = (Card)cards[0];
                cards.RemoveAt(0);
            }
            else
            {
                Console.WriteLine("Ошибка. Колода пуста.");
            }
            return firstCard;
        }

        public void pushCard(Card card)
        {
            if (card != null)
                cards.Add(card);
        }

        public void clearDeck()
        {
            if (cards.Count != 0)
                cards.Clear();
        }

        public int getCardCount()
        {
            return cards.Count;
        }

        public int getPoints()
        {
            int points = 0;
            if (cards.Count != 0)
            {
                foreach (Card card in cards)
                {
                    points += card.Point;
                }
            }
            return points;
        }

        public void drawDeck(int leftOrigin, int topOrigin, int cardOffset)
        {
            if (cards.Count != 0)
            {
                int index = 0;
                int currentCardTop, currentCardLeft;
                foreach (Card card in cards)
                {
                    try
                    {
                        currentCardTop = topOrigin;
                        currentCardLeft = leftOrigin + index * cardOffset;
                        card.drawCard(currentCardLeft, currentCardTop);
                        index++;
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("Ошибка: {0}", e.Message);
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Колода пуста");
            }
        }

        public void setCardState(Card.STATE state)
        {
            foreach (Card card in cards)
            {
                card.State = state;
            }
        }

        //Debug

        public void printDeck()
        {
            if (cards.Count != 0)
            {
                foreach (Card card in cards)
                {
                    Console.Write("{0}{1}\t", card.Face, card.Suit);
                }
            }
            else
            {
                Console.WriteLine("Колода пуста");
            }
        }
    }
}
