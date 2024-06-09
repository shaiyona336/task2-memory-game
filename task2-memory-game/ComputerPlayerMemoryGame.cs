using System;

namespace task2_memory_game
{
    class ComputerPlayerMemoryGame : PlayerMemoryGame
    {
        private Random m_RandomNumGenerator;

        public ComputerPlayerMemoryGame() : base()
        {
            m_RandomNumGenerator = new Random();
        }

        public override ((int, int), (int, int)) PickTwoCardsOnBoard(BoardMemoryGame board, out bool continueGame)
        {
            (int, int) pair1 = generateRandomPair(board);
            (int, int) pair2 = generateRandomPair(board);
            continueGame = true;

            while (!board.IsCardValid(pair1))
            {
                pair1 = generateRandomPair(board);
            }

            while (pair2 == pair1 || !board.IsCardValid(pair2))
            {
                pair2 = generateRandomPair(board);
            }

            return (pair1, pair2);
        }

        private (int, int) generateRandomPair(BoardMemoryGame board)
        {
            int row = m_RandomNumGenerator.Next(board.BoardHeight);
            int col = m_RandomNumGenerator.Next(board.BoardWidth);

            return (row, col);
        }
    }
}