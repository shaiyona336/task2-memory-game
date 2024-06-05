

using System;
using System.Collections.Generic;

namespace task2_memory_game
{
    public class BoardMemoryGame
    {
        private int m_sizeRowBoard;
        private int m_sizeColumnBoard;
        private MemoryCard[,] boardState;

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
            List<MemoryCard> pairsToShuffle = new List<MemoryCard>(); //save all the cards in a list, shuffle them, and put them in the board


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
            int tempCurrCardValue;

            for (int card = 0; card < list.Count; card++)
            {
                whoToSwitchWith = random.Next(list.Count + 1);
                tempCurrCardValue = list[card].getNumberOfPair();
                list[card].setNumberOfPair(list[whoToSwitchWith].getNumberOfPair());
                list[whoToSwitchWith].setNumberOfPair(tempCurrCardValue);
            }

        }

    }
}
