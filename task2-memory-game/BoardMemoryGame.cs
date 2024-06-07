using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace task2_memory_game
{
    public class BoardMemoryGame
    {
        private int m_sizeRowBoard;
        private int m_sizeColumnBoard;
        private MemoryCard[,] boardState;
        private bool UserOpenedOneCard = false;
        private int withRowCardUserOpen;
        private int withColumnCardUserOpen;


        public void setCardsOpenTwoSeconds((int,int) firstCard, (int,int) secondCard)
        {
            boardState[firstCard.Item1, firstCard.Item2].setIsSeen(true);
            boardState[secondCard.Item1, secondCard.Item2].setIsSeen(true);
        }

        public void setCardsClosedTwoSeconds((int, int) firstCard, (int, int) secondCard)
        {
            boardState[firstCard.Item1, firstCard.Item2].setIsSeen(false);
            boardState[secondCard.Item1, secondCard.Item2].setIsSeen(false);
        }


        public void setCardUserOpenAsSeen()
        {
            if (UserOpenedOneCard)
            {
                boardState[withRowCardUserOpen, withColumnCardUserOpen].setIsSeen(true);
            }
        }

        public void setCardUserOpenAsUnseen()
        {
            if (UserOpenedOneCard)
            {
                boardState[withRowCardUserOpen, withColumnCardUserOpen].setIsSeen(false);
            }
        }


        public bool getUserOpenedOneCard()
        {
            return UserOpenedOneCard;
        }


        public int getBoardXDimension()
        {
            return m_sizeColumnBoard;
        }

        public int getBoardYDimension()
        {  
            return m_sizeRowBoard;
        }

        public BoardMemoryGame(int i_sizeRowBoard, int i_sizeColumnBoard)
        {
            m_sizeRowBoard = i_sizeRowBoard;
            m_sizeColumnBoard = i_sizeColumnBoard;
            boardState = new MemoryCard[m_sizeRowBoard, m_sizeColumnBoard];
        }

        public MemoryCard[,] getBoardState()
        {
            return boardState;
        }

        public void generatePairs()
        {
            int numberOfPairs = (m_sizeRowBoard * m_sizeColumnBoard) / 2; //size of the board devide by two
            List<MemoryCard> pairsToShuffle = new List<MemoryCard>(numberOfPairs * 2); //save all the cards in a list, shuffle them, and put them in the board

            for (int pair = 1; pair <= numberOfPairs; pair++)
            {
                MemoryCard card1 = new MemoryCard(pair);
                MemoryCard card2 = new MemoryCard(pair);
                pairsToShuffle.Add(card1);
                pairsToShuffle.Add(card2);
            }

            //shuffle
            shufflePairs(pairsToShuffle);
            //put the cards in the board
            for (int row = 0; row < m_sizeRowBoard; row++)
            {
                for (int column = 0; column < m_sizeColumnBoard; column++)
                {
                    boardState[row, column] = pairsToShuffle[row * m_sizeColumnBoard + column];
                }
            }

        }

        private void shufflePairs(List<MemoryCard> list)
        {
            Random random = new Random();
            int whoToSwitchWith;

            for (int card = 0; card < list.Count; card++)
            {
                whoToSwitchWith = random.Next(list.Count + 1);
                switchCardsInList(list, card, whoToSwitchWith);
            }

        }

        private void switchCardsInList(List<MemoryCard> io_list, int i_card1Index, int i_card2Index)
        {
            MemoryCard tempCard = io_list[i_card1Index];
            io_list[i_card1Index] = io_list[i_card2Index];
            io_list[i_card2Index] = tempCard;
        }

        public bool openCardInBoard(int i_row, int i_column) //return if flipped a pair
        {
            bool isFlippedAPair = false;
            if (UserOpenedOneCard == true)
            {
                if (boardState[i_row, i_column].getNumberOfPair() == boardState[withRowCardUserOpen, withColumnCardUserOpen].getNumberOfPair())
                {
                    isFlippedAPair = true;
                    boardState[i_row, i_column].setIsSeen(true); // need to flip this card and keep his state like that
                    boardState[withRowCardUserOpen, withColumnCardUserOpen].setIsSeen(true);
                }
                else //if the card flipped wasnt a pair
                {
                    isFlippedAPair = false;
                    boardState[withRowCardUserOpen, withColumnCardUserOpen].setIsSeen(false); // need to set the old card that been flipped to not seen again
                }
            }
            else //if (UserOpenedOneCard == false), then need to set the card that is now opened in the middle of a turn
            {
                withRowCardUserOpen = i_row;
                withColumnCardUserOpen = i_column;
            }
            UserOpenedOneCard = !UserOpenedOneCard; //if user open card, now need to flip the condition of one card open in a turn
            return isFlippedAPair;
        }

        public bool isCardValid(int i_row, int i_column)
        {
            bool i_flag = false;

            if (i_row >= 0 && i_row <= m_sizeRowBoard && i_column >= 0 && i_column <= m_sizeColumnBoard)
            {
                if (!(boardState[i_row, i_column].getIsSeen()))
                {
                    i_flag = true;
                }
            }
            return i_flag;
        }



        public void lineOfEquals(int i_columns)
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
            for (int latter = 0; latter < m_sizeColumnBoard; latter++)
            {
                Console.Write((char)('A' + latter) + " ");
            }
            Console.WriteLine();
            //top border
            lineOfEquals(m_sizeColumnBoard);
            //rows
            for (int row = 1; row <= m_sizeRowBoard; row++)
            {
                Console.Write(row + " |");

                for (int column = 0; column < m_sizeColumnBoard; column++)
                {
                    if (!(getBoardState()[row - 1, column].getIsSeen()))
                    {
                        Console.Write(" |");
                    }
                    else
                    {
                        Console.Write((getBoardState()[row - 1, column].getNumberOfPair()) + "|");
                    }
                }
                Console.WriteLine();
                lineOfEquals(m_sizeColumnBoard);
            }
            setCardUserOpenAsUnseen();
        }


    }
}
