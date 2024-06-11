using System;
using System.Collections;
using System.Collections.Generic;

namespace MemoryGameLogic
{
    class ComputerPlayerMemoryGame : PlayerMemoryGame
    {
        private Random m_RandomNumGenerator;
        private MemoryGameCardCords?[] m_SeenCardsArr;
        private Queue<MemoryGameCardCords> m_NextCardsToPlayQueue;
        private bool m_IsInTheMiddleOfATurn = false;
        public bool DidNewGameStarted { private get; set; }
        public const string k_ComputerName = "Computer";

        public ComputerPlayerMemoryGame() : base()
        {
            Name = k_ComputerName;
            m_RandomNumGenerator = new Random();
            m_NextCardsToPlayQueue = new Queue<MemoryGameCardCords>(3);
            DidNewGameStarted = true;
        }

        public override MemoryGameCardCords PickCardOnBoard(BoardMemoryGame i_Board, out bool o_ContinueGame)
        {
            o_ContinueGame = true;
            setCardsArr(i_Board.BoardHeight * i_Board.BoardWidth);

            removeAllIllegalCardsFromQueue(i_Board);

            if (m_NextCardsToPlayQueue.Count == 0)
            {
                checkSeenCardsAndAddCardsToQueue(i_Board);
            }

            m_IsInTheMiddleOfATurn = !m_IsInTheMiddleOfATurn;
            return m_NextCardsToPlayQueue.Dequeue();
        }

        private void checkSeenCardsAndAddCardsToQueue(BoardMemoryGame i_Board)
        {
            MemoryGameCardCords generatedCardCords = generateRandomLegalCardCords(i_Board);
            m_NextCardsToPlayQueue.Enqueue(generatedCardCords);
            int generatedCardIndex = getCardIndexInSeenCardsArr(generatedCardCords, i_Board);
            ref MemoryGameCardCords? generatedCardInSeenArr = ref m_SeenCardsArr[generatedCardIndex];

            if (generatedCardInSeenArr == null)
            {
                generatedCardInSeenArr = generatedCardCords;
            }
            else if (!generatedCardCords.IsEqualTo((MemoryGameCardCords)generatedCardInSeenArr))
            {
                m_NextCardsToPlayQueue.Enqueue((MemoryGameCardCords)generatedCardInSeenArr);
                if (m_IsInTheMiddleOfATurn)
                {
                    m_NextCardsToPlayQueue.Enqueue(generatedCardCords);
                }
            }
        }

        private MemoryGameCardCords generateRandomLegalCardCords(BoardMemoryGame i_Board)
        {
            MemoryGameCardCords generatedCardCords = generateRandomCardCords(i_Board);

            while (!i_Board.IsCardHidden(generatedCardCords))
            {
                generatedCardCords = generateRandomCardCords(i_Board);
            }

            return generatedCardCords;
        }

        private MemoryGameCardCords generateRandomCardCords(BoardMemoryGame i_Board)
        {
            int row = m_RandomNumGenerator.Next(i_Board.BoardHeight);
            int col = m_RandomNumGenerator.Next(i_Board.BoardWidth);

            return (row, col);
        }

        private void setCardsArr(int i_NumberOfSquaresOnBoard)
        {
            if (DidNewGameStarted)
            {
                m_SeenCardsArr = new MemoryGameCardCords?[i_NumberOfSquaresOnBoard];
                DidNewGameStarted = false;
            }
        }

        private void removeAllIllegalCardsFromQueue(BoardMemoryGame i_BoardOfCardsInQueue)
        {
            int numOfItemsInQueue = m_NextCardsToPlayQueue.Count;
            for (int i = 0; i < numOfItemsInQueue; i++)
            {
                MemoryGameCardCords currentCardInQueue = m_NextCardsToPlayQueue.Dequeue();
                if (i_BoardOfCardsInQueue.IsCardHidden(currentCardInQueue))
                {
                    m_NextCardsToPlayQueue.Enqueue(currentCardInQueue);
                }
            }
        }

        private int getCardIndexInSeenCardsArr(MemoryGameCardCords i_Card, BoardMemoryGame i_BoardOfCard)
        {
            return i_BoardOfCard.GetCardOnBoard(i_Card).PairNum - 1;
        }
    }
}