using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace task2_memory_game
{
    public class BoardMemoryGame
    {
        public int BoardHeight { get; private set; }
        public int BoardWidth { get; private set; }
        private MemoryCard[,] boardState;
        private bool UserOpenedOneCard = false;
        private int withRowCardUserOpen;
        private int withColumnCardUserOpen;


        public void RevealCards((int,int) firstCard, (int,int) secondCard)
        {
            boardState[firstCard.Item1, firstCard.Item2].IsSeen = true;
            boardState[secondCard.Item1, secondCard.Item2].IsSeen = true;
        }

        public void HideCards((int, int) firstCard, (int, int) secondCard)
        {
            boardState[firstCard.Item1, firstCard.Item2].IsSeen = false;
            boardState[secondCard.Item1, secondCard.Item2].IsSeen = false;
        }

        public void setCardUserOpenAsSeen()
        {
            if (UserOpenedOneCard)
            {
                boardState[withRowCardUserOpen, withColumnCardUserOpen].IsSeen = true;
            }
        }

        public void setCardUserOpenAsUnseen()
        {
            if (UserOpenedOneCard)
            {
                boardState[withRowCardUserOpen, withColumnCardUserOpen].IsSeen = false;
            }
        }

        public bool getUserOpenedOneCard()
        {
            return UserOpenedOneCard;
        }

        public BoardMemoryGame(int i_BoardHeight, int i_BoardWidth)
        {
            BoardHeight = i_BoardHeight;
            BoardWidth = i_BoardWidth;
            boardState = new MemoryCard[BoardHeight, BoardWidth];
        }

        public MemoryCard[,] getBoardState()
        {
            return boardState;
        }

        public void generatePairs()
        {
            int numberOfPairs = (BoardHeight * BoardWidth) / 2; //size of the board devide by two
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
            for (int row = 0; row < BoardHeight; row++)
            {
                for (int column = 0; column < BoardWidth; column++)
                {
                    boardState[row, column] = pairsToShuffle[row * BoardWidth + column];
                }
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

        public bool openCardInBoard(int i_row, int i_column) //return if flipped a pair
        {
            bool isFlippedAPair = false;
            if (UserOpenedOneCard == true)
            {
                if (boardState[i_row, i_column].PairNum == boardState[withRowCardUserOpen, withColumnCardUserOpen].PairNum)
                {
                    isFlippedAPair = true;
                    boardState[i_row, i_column].IsSeen = true; // need to flip this card and keep his state like that
                    boardState[withRowCardUserOpen, withColumnCardUserOpen].IsSeen = true;
                }
                else //if the card flipped wasnt a pair
                {
                    isFlippedAPair = false;
                    boardState[withRowCardUserOpen, withColumnCardUserOpen].IsSeen = false; // need to set the old card that been flipped to not seen again
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

            if (i_row >= 0 && i_row <= BoardHeight && i_column >= 0 && i_column <= BoardWidth)
            {
                if (!(boardState[i_row, i_column].IsSeen))
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
            for (int latter = 0; latter < BoardWidth; latter++)
            {
                Console.Write((char)('A' + latter) + " ");
            }
            Console.WriteLine();
            //top border
            lineOfEquals(BoardWidth);
            //rows
            for (int row = 1; row <= BoardHeight; row++)
            {
                Console.Write(row + " |");

                for (int column = 0; column < BoardWidth; column++)
                {
                    if (!(getBoardState()[row - 1, column].IsSeen))
                    {
                        Console.Write(" |");
                    }
                    else
                    {
                        Console.Write((getBoardState()[row - 1, column].PairNum) + "|");
                    }
                }
                Console.WriteLine();
                lineOfEquals(BoardWidth);
            }
            setCardUserOpenAsUnseen();
        }


    }
}
