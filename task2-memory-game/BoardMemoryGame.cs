using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace task2_memory_game
{
    public class BoardMemoryGame
    {
        public int BoardHeight { get; private set; }
        public int BoardWidth { get; private set; }
        public MemoryCard[,] BoardState { get; private set; }
        public bool IsThereARevealedCard { get; private set; }

        private (int, int) m_currentlyOpenedCardCords;
        private MemoryCard CurrentlyOpenedCard
        {
            get
            {
                return BoardState[m_currentlyOpenedCardCords.Item1, m_currentlyOpenedCardCords.Item2];
            }
        }

        public BoardMemoryGame(int i_BoardHeight, int i_BoardWidth) : this((i_BoardHeight, i_BoardWidth))
        {
        }

        public BoardMemoryGame((int, int) i_BoardDimensions)
        {
            setEmptyBoard(i_BoardDimensions);
        }

        public void setEmptyBoard((int, int) i_BoardDimensions)
        {
            BoardHeight = i_BoardDimensions.Item1;
            BoardWidth = i_BoardDimensions.Item2;
            BoardState = new MemoryCard[BoardHeight, BoardWidth];
            IsThereARevealedCard = false;
        }

        public void RevealCards((int,int) i_firstCardCords, (int,int) i_secondCardCords)
        {
            BoardState[i_firstCardCords.Item1, i_firstCardCords.Item2].RevealCard();
            BoardState[i_firstCardCords.Item1, i_firstCardCords.Item2].IsSeen = true; //It's the same!!!
            BoardState[i_secondCardCords.Item1, i_secondCardCords.Item2].IsSeen = true;
            BoardState[i_secondCardCords.Item1, i_secondCardCords.Item2].RevealCard();
        }

        public void HideCards((int, int) firstCardCords, (int, int) secondCardCords)
        {
            BoardState[firstCardCords.Item1, firstCardCords.Item2].HideCard();
            BoardState[secondCardCords.Item1, secondCardCords.Item2].HideCard();
            BoardState[firstCardCords.Item1, firstCardCords.Item2].IsSeen = false; //it's the same thing!!!!!
            BoardState[secondCardCords.Item1, secondCardCords.Item2].IsSeen = false;
        }

        public void setCardUserOpenAsSeen()
        {
            if (IsThereARevealedCard)
            {
                CurrentlyOpenedCard.RevealCard();
                BoardState[m_currentlyOpenedCardCords.Item1, m_currentlyOpenedCardCords.Item2].IsSeen = true;
            }
        }

        public void setCardUserOpenAsUnseen()
        {
            if (IsThereARevealedCard)
            {
                CurrentlyOpenedCard.HideCard();
                BoardState[m_currentlyOpenedCardCords.Item1, m_currentlyOpenedCardCords.Item2].IsSeen = true;
            }
        }

        public void GeneratePairs()
        {
            int numberOfPairs = (BoardHeight * BoardWidth) / 2; //size of the board devide by two
            insertPairsToList(numberOfPairs, out List<MemoryCard> pairsToShuffle);//save all the cards in a list, shuffle them, and put them in the board
            shufflePairs(pairsToShuffle);

            //put the cards on the board
            for (int row = 0; row < BoardHeight; row++)
            {
                for (int column = 0; column < BoardWidth; column++)
                {
                    BoardState[row, column] = pairsToShuffle[row * BoardWidth + column];
                }
            }
        }

        private void insertPairsToList(int i_NumberOfPairs, out List<MemoryCard> o_PairsList)
        {
            o_PairsList = new List<MemoryCard>(i_NumberOfPairs * 2);

            for (int pair = 1; pair <= i_NumberOfPairs; pair++)
            {
                MemoryCard card1 = new MemoryCard(pair);
                MemoryCard card2 = new MemoryCard(pair);
                o_PairsList.Add(card1);
                o_PairsList.Add(card2);
            }
        }

        private void shufflePairs(List<MemoryCard> list)
        {
            Random random = new Random();
            int whoToSwitchWith;

            for (int card = 0; card < list.Count; card++)
            {
                whoToSwitchWith = random.Next(list.Count);
                switchCardsInList(list, card, whoToSwitchWith);
            }
        }

        private void switchCardsInList(List<MemoryCard> io_list, int i_card1Index, int i_card2Index)
        {
            MemoryCard tempCard = io_list[i_card1Index];
            io_list[i_card1Index] = io_list[i_card2Index];
            io_list[i_card2Index] = tempCard;
        }

        public bool flipCardOnBoard(int i_row, int i_column) //return if flipped a pair
        {
            bool isFlippedAPair = false;
            if (IsThereARevealedCard == true)
            {
                if (BoardState[i_row, i_column].PairNum == CurrentlyOpenedCard.PairNum)
                {
                    isFlippedAPair = true;
                    BoardState[i_row, i_column].RevealCard(); // need to flip this card and keep his state like that
                    CurrentlyOpenedCard.RevealCard();
                }
                else //if the card flipped wasnt a pair
                {
                    isFlippedAPair = false;
                    CurrentlyOpenedCard.HideCard(); // need to set the old card that been flipped to not seen again
                }
            }
            else //if (UserOpenedOneCard == false), then need to set the card that is now opened in the middle of a turn
            {
                m_currentlyOpenedCardCords = (i_row, i_column);
            }

            IsThereARevealedCard = !IsThereARevealedCard; //if user open card, now need to flip the condition of one card open in a turn
            return isFlippedAPair;
        }

        public bool IsCardValid(int i_row, int i_column)
        {
            bool returnValue = false;

            if (i_row >= 0 && i_row <= BoardHeight && i_column >= 0 && i_column <= BoardWidth)
            {
                if (!BoardState[i_row, i_column].IsSeen)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }

        public bool IsCardValid((int,int) i_Pair)
        {
            return IsCardValid(i_Pair.Item1, i_Pair.Item2);
        }

        private void printLineOfEquals(int i_columns)
        {
            Console.Write("  ");
            for (int amountOfEquals = 0; amountOfEquals < i_columns * 2 + 1; amountOfEquals++)
            {
                Console.Write('=');
            }
            Console.WriteLine();
        }

        public void printBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            setCardUserOpenAsSeen(); //treat the open card in the current turn as a normal opened card to show him on the board
            Console.Write("   ");
            for (int latter = 0; latter < BoardWidth; latter++)
            {
                Console.Write((char)('A' + latter) + " ");
            }
            Console.WriteLine();
            //top border
            printLineOfEquals(BoardWidth);
            //rows
            for (int row = 1; row <= BoardHeight; row++)
            {
                Console.Write(row + " |");

                for (int column = 0; column < BoardWidth; column++)
                {
                    if (!(BoardState[row - 1, column].IsSeen))
                    {
                        Console.Write(" |");
                    }
                    else
                    {
                        Console.Write((char)((BoardState[row - 1, column].PairNum) + 'A' - 1) + "|");
                    }
                }
                Console.WriteLine();
                printLineOfEquals(BoardWidth);
            }
            setCardUserOpenAsUnseen();
        }

        public bool IsBoardFullyRevealed()
        {
            bool returnValue = true;
            for (int boardXDimension = 0; boardXDimension < BoardWidth && returnValue; boardXDimension++)
            {
                for (int boardYDimension = 0; boardYDimension < BoardHeight && returnValue; boardYDimension++)
                {
                    if (!BoardState[boardXDimension, boardYDimension].IsSeen)
                    {
                        returnValue = false;
                        break;
                    }
                }

            }
            return returnValue;
        }

        public bool IsPairOnGameBoard((int, int) i_Pair) //move to board
        {
            bool returnValue = true;

            if (i_Pair.Item1 > BoardHeight || i_Pair.Item1 < 0 || i_Pair.Item2 > BoardWidth || i_Pair.Item2 < 0)
            {
                returnValue = false;
            }
            return returnValue;
        }


    }
}
